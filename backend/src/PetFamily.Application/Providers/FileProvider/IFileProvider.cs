using CSharpFunctionalExtensions;
using PetFamily.Application.Volunteers.AddFiles;
using PetFamily.Application.Volunteers.DeleteFiles;
using PetFamily.Application.Volunteers.GetFiles;
using PetFamily.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> Upload(AddFilesCommand command, CancellationToken cancellationToken);

        Task<Result<string, Error>> GetByName(
            GetFilesCommand command,
            CancellationToken cancellationToken);

        Task<Result<string, Error>> Delete(
            DeleteFilesCommand command,
            CancellationToken cancellationToken);
    }
}
