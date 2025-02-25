using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Extensions;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.GetFiles;

public class GetFilesHandler : ICommandHandler<string, GetFilesCommand>
{
    private readonly IFileProvider _fileProvider;
    private readonly IValidator<GetFilesCommand> _validator;
    private readonly ILogger<GetFilesHandler> _logger;

    public GetFilesHandler(
        IFileProvider fileProvider,
        IValidator<GetFilesCommand> validator,
        ILogger<GetFilesHandler> logger)
    {
        _fileProvider = fileProvider;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<string, ErrorList>> Handle(
        GetFilesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if(validationResult.IsValid == false)
            return validationResult.ToErrorList(_logger, "get", "pet files");

        var result = await _fileProvider.GetByName(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToErrorList();

        _logger.LogInformation(
            "The link to download the file named '{name}' from MinIO has been formed", result.Value);

        return result.Value;
    }
}
