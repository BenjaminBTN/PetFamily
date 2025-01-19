using CSharpFunctionalExtensions;
using PetFamily.Application.Providers.FileProvider.Dtos;
using PetFamily.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> Upload(FileDto fileDto, CancellationToken cancellationToken);
    }
}
