using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.AddFiles
{
    public class AddFilesHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<AddFilesHandler> _logger;

        public AddFilesHandler(
            IVolunteersRepository volunteersRepository,
            IUnitOfWork unitOfWork,
            IFileProvider fileProvider,
            ILogger<AddFilesHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            AddFilesCommand command,
            CancellationToken cancellationToken)
        {
            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteer = await _volunteersRepository.GetById(volunteerId, cancellationToken);
            if(volunteer.IsFailure)
                return volunteer.Error.ToErrorList();

            var petId = PetId.Create(command.PetId);
            var pet = volunteer.Value.GetPetById(petId);
            if(pet.IsFailure)
                return pet.Error.ToErrorList();

            // upload files
            var uploadResult = await _fileProvider.Upload(command.Files, command.BucketName, cancellationToken);
            if(uploadResult.IsFailure)
                return uploadResult.Error.ToErrorList();

            // add paths in repository
            var photos = uploadResult.Value
                .Select(o => new Photo(o))
                .ToList();

            if(photos == null || photos.Count == 0)
            {
                _logger.LogError("Fail to add files in repository");
                return Error.Failure("file.upload", "Error in placing files in the repository").ToErrorList();
            }

            pet.Value.UpdatePetPhotos(new(photos));

            await _unitOfWork.SaveChanges(cancellationToken);

            var paths = string.Join(", ", photos.Select(p => p.PathToStorage.Value));

            _logger.LogInformation(
                "New files was upload to MinIO with the names: {names}", paths);

            return paths;
        }
    }
}
