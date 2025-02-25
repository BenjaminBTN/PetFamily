namespace PetFamily.Domain.Shared;

public abstract class Entity<TId> where TId : notnull
{
    public TId Id { get; private set; }

    protected Entity(TId id) => Id = id;        
}