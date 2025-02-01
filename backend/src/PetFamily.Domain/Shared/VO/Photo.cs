using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Domain.Shared.VO
{
    public record Photo
    {
        public FilePath PathToStorage { get; }
        public bool IsMain { get; }

        public Photo(FilePath pathToStorage, bool isMain = false)
        {
            PathToStorage = pathToStorage;
            IsMain = isMain;
        }
    }
}