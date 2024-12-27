namespace PetFamily.Domain.Shared.Interfaces
{
    public interface IDeletable
    {
        void Delete();

        void Restore();
    }
}
