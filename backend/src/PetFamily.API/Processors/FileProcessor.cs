using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Processors
{
    public class FileProcessor : IAsyncDisposable
    {
        private readonly List<UploadFileDto> _filesDto = [];

        public async ValueTask DisposeAsync()
        {
            foreach(var file in _filesDto)
            {
                await file.Stream.DisposeAsync();
            }
        }

        public List<UploadFileDto> Process(IFormFileCollection files, CancellationToken cancellationToken)
        {
            foreach(var file in files)
            {
                var ext = Path.GetExtension(file.FileName);
                var objectName = FilePath.Create(Guid.NewGuid(), ext);
                var content = file.OpenReadStream();

                var fileDto = new UploadFileDto(content, objectName.Value);

                _filesDto.Add(fileDto);
            }

            return _filesDto;
        }
    }
}
