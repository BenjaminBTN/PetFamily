using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.DeleteFiles
{
    public class DeleteFilesHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<DeleteFilesHandler> _logger;

        public DeleteFilesHandler(IFileProvider fileProvider, ILogger<DeleteFilesHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            DeleteFilesCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _fileProvider.Delete(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToErrorList();

            _logger.LogInformation("The file named: {name} has been successfully deleted from MinIO ", result.Value);

            return result.Value;
        }
    }
}
