using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.DeletePetPhotos
{
    public class DeleteFilesHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteFilesHandler> _logger;

        public DeleteFilesHandler(
            IFileProvider fileProvider,
            ILogger<DeleteFilesHandler> logger,
            IVolunteersRepository volunteersRepository,
            IUnitOfWork unitOfWork)
        {
            _fileProvider = fileProvider;
            _logger = logger;
            _volunteersRepository = volunteersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string, ErrorList>> Handle(
            DeletePetPhotosCommand command,
            CancellationToken cancellationToken)
        {
            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteer = await _volunteersRepository.GetById(volunteerId, cancellationToken);
            if(volunteer.IsFailure)
                return volunteer.Error.ToErrorList();

            var petId = PetId.Create(command.PetId);
            var petResult = volunteer.Value.GetPetById(petId);
            if(petResult.IsFailure)
                return petResult.Error.ToErrorList();

            var pet = petResult.Value;

            var deleteResult = await _fileProvider.Delete(command, cancellationToken);
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
                    return Errors.General.NotFound("Pet photo with name: " + name).ToErrorList();
                }
            }

            pet.UpdatePetPhotos(petPhotos);

            await _unitOfWork.SaveChanges(cancellationToken);

            var paths = string.Join(", ", deleteResult.Value);

            _logger.LogInformation(
                "The file(s) named: {name} has been successfully deleted from MinIO ", paths);

            return paths;
        }
    }
}
