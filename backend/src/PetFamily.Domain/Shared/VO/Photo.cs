using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.VO
{
    public record Photo
    {
        public string PathToStorage { get; }
        public bool IsMain { get; }

        private Photo(string pathToStorage, bool isMain)
        {
            PathToStorage = pathToStorage;
            IsMain = isMain;
        }

        public Result<Photo, Error> Create(string pathToStorage, bool isMain = default)
        {
            if(string.IsNullOrWhiteSpace(pathToStorage))
                return Errors.General.NullValue("Path");

            return new Photo(pathToStorage, isMain);
        }
    }
}