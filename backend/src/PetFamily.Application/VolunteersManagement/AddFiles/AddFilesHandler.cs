using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.AddFiles
{
    public class AddFilesHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<AddFilesHandler> _logger;

        public AddFilesHandler(
            IFileProvider fileProvider,
            ILogger<AddFilesHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            AddFilesCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _fileProvider.Upload(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToErrorList();

            _logger.LogInformation("New file was upload to MinIO with the name: {name}", command.ObjectName);

            return result.Value;
        }
    }
}
