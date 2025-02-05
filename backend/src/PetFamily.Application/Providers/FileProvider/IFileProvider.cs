using CSharpFunctionalExtensions;
using PetFamily.Application.VolunteersManagement.DeleteFiles;
using PetFamily.Application.VolunteersManagement.GetFiles;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<List<FilePath>, Error>> Upload(
            IEnumerable<UploadFileData> files,
            string bucketName, 
            CancellationToken cancellationToken);

        Task<Result<string, Error>> GetByName(
            GetFilesCommand command,
            CancellationToken cancellationToken);

        Task<Result<List<string>, Error>> Delete(
            DeleteFilesCommand command,
            CancellationToken cancellationToken);
    }
}
