using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers
{
    public record PetPhoto
    {
        public string PathToStorage { get; }
        public bool IsMain { get; }

        private PetPhoto(string pathToStorage, bool isMain)
        {
            PathToStorage = pathToStorage;
            IsMain = isMain;
        }

        public Result<PetPhoto, Error> Create(string pathToStorage, bool isMain = default)
        {
            if (string.IsNullOrWhiteSpace(pathToStorage))
                return Errors.General.NullValue("Path");

            return new PetPhoto(pathToStorage, isMain);
        }
    }
}