using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.GetFiles
{
    public class GetFilesHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<GetFilesHandler> _logger;

        public GetFilesHandler(
            IFileProvider fileProvider,
            ILogger<GetFilesHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            GetFilesCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _fileProvider.GetByName(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToErrorList();

            _logger.LogInformation(
                "The link to download the file named: {name} from MinIO has been formed", result.Value);

            return result.Value;
        }
    }
}
