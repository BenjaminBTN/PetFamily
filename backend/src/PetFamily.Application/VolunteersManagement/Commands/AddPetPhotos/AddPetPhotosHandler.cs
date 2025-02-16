using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.AddPetPhotos
{
    public class AddPetPhotosHandler : ICommandHandler<string, AddPetPhotosCommand>
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileProvider _fileProvider;
        private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
        private readonly IValidator<AddPetPhotosCommand> _validator;
        private readonly ILogger<AddPetPhotosHandler> _logger;

        public AddPetPhotosHandler(
            IVolunteersRepository volunteersRepository,
            IUnitOfWork unitOfWork,
            IFileProvider fileProvider,
            IMessageQueue<IEnumerable<FileInfo>> messageQueue,
            IValidator<AddPetPhotosCommand> validator,
            ILogger<AddPetPhotosHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
            _messageQueue = messageQueue;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            AddPetPhotosCommand command,
            CancellationToken cancellationToken)
        {
            // validation
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "add", "pet photos");

            // create VOs
            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteer = await _volunteersRepository.GetById(volunteerId, cancellationToken);
            if(volunteer.IsFailure)
                return volunteer.Error.ToErrorList();

            var petId = PetId.Create(command.PetId);
            var pet = volunteer.Value.GetPetById(petId);
            if(pet.IsFailure)
                return pet.Error.ToErrorList();

            // upload files
            var uploadResult = await _fileProvider.UploadObjects(command.Files, command.BucketName, cancellationToken);
            if(uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(command.Files
                    .Select(f => new FileInfo(f.ObjectName.Value, command.BucketName)), cancellationToken);
                return uploadResult.Error.ToErrorList();
            }

            // add paths in repository
            var exitingPhotos = pet.Value.PetPhotos.Photos.AsEnumerable();

            var newPhotos = uploadResult.Value
                .Select(o => new Photo(o))
                .ToList();

            if(newPhotos == null || newPhotos.Count == 0)
            {
                _logger.LogError("Fail to add files in repository");
                return Error.Failure("file.upload", "Error in placing files in the repository").ToErrorList();
            }

            var allPhotos = exitingPhotos.Concat(newPhotos).Select(p => p.PathToStorage);
            pet.Value.UpdatePetPhotos(allPhotos);

            // ending
            await _unitOfWork.SaveChanges(cancellationToken);

            var paths = string.Join(", ", newPhotos.Select(p => p.PathToStorage.Value));

            _logger.LogInformation(
                "New files was upload to MinIO with the names: {names}", paths);

            return paths;
        }
    }
}
