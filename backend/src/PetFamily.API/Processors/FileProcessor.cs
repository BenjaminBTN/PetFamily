using CSharpFunctionalExtensions;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Processors;

public class FileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileData> _filesDto = [];

    public async ValueTask DisposeAsync()
    {
        foreach(var file in _filesDto)
        {
            await file.Stream.DisposeAsync();
        }
    }

    public Result<List<UploadFileData>, Error> Process(IFormFileCollection files, CancellationToken cancellationToken)
    {
        foreach(var file in files)
        {
            var ext = Path.GetExtension(file.FileName.ToLower());
            if(ext == null || ext == string.Empty)
                return Errors.General.InvalidValue("File extension");

            var objectNameResult = FilePath.Create(Guid.NewGuid(), ext);
            if(objectNameResult.IsFailure)
                return objectNameResult.Error;

            var content = file.OpenReadStream();

            var fileDto = new UploadFileData(content, objectNameResult.Value);

            _filesDto.Add(fileDto);
        }

        return _filesDto;
    }
}
