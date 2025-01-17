using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> Upload();
    }
}
