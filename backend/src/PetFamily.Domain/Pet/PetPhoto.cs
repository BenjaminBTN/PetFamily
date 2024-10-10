using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pet
{
    public record PetPhoto
    {
        private PetPhoto(string pathToStorage, bool isMain)
        {
            PathToStorage = pathToStorage;
            IsMain = isMain;
        }

        private string PathToStorage { get; }
        private bool IsMain { get; } = default!;

        public Result<PetPhoto> Create(string pathToStorage, bool isMain)
        {
            if(String.IsNullOrWhiteSpace(pathToStorage))
                return Result.Failure<PetPhoto>("Path can not be empty");

            return Result.Success<PetPhoto>(new PetPhoto(pathToStorage, isMain));
        }
    }
}
