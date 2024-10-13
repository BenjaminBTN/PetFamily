using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
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
            if (string.IsNullOrWhiteSpace(pathToStorage))
                return Result.Failure<PetPhoto>("Path can not be empty");

            return Result.Success(new PetPhoto(pathToStorage, isMain));
        }
    }
}
