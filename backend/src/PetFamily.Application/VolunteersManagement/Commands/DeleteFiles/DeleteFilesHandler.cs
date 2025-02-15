using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.DeleteFiles
{
    public class DeleteFilesHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileProvider _fileProvider;
        private readonly IValidator<DeleteFilesCommand> _validator;
        private readonly ILogger<DeleteFilesHandler> _logger;

        public DeleteFilesHandler(
            IVolunteersRepository volunteersRepository,
            IUnitOfWork unitOfWork,
            IFileProvider fileProvider,
            IValidator<DeleteFilesCommand> validator,
            ILogger<DeleteFilesHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            DeleteFilesCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "delete", "pet files");

            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteer = await _volunteersRepository.GetById(volunteerId, cancellationToken);
            if(volunteer.IsFailure)
                return volunteer.Error.ToErrorList();

            var petId = PetId.Create(command.PetId);
            var petResult = volunteer.Value.GetPetById(petId);
            if(petResult.IsFailure)
                return petResult.Error.ToErrorList();

            var pet = petResult.Value;

            var filesToDelete = command.ObjectNames.Select(o => new FileInfo(o, command.BucketName));

            var deleteResult = await _fileProvider.Delete(filesToDelete, cancellationToken);
            if(deleteResult.IsFailure)
                return deleteResult.Error.ToErrorList();

            var petPhotos = pet.PetPhotos.Photos.Select(p => p.PathToStorage).ToList();

            foreach(var name in command.ObjectNames)
            {
                try
                {
                    var filePath = petPhotos.Single(p => p.Value == name);
                    petPhotos.Remove(filePath);
                }
                catch(Exception)
                {
                    _logger.LogError("Pet photo with name '{name}' not found", name);
                    return Errors.General.NotFound("Pet photo with name: " + name).ToErrorList();
                }
            }

            pet.UpdatePetPhotos(petPhotos);

            await _unitOfWork.SaveChanges(cancellationToken);

            var paths = string.Join(", ", deleteResult.Value);

            _logger.LogInformation(
                "The file(s) named '{name}' has been successfully deleted from MinIO", paths);

            return paths;
        }
    }
}
