using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers
{
    public record PetRequisites
    {
        public string Name { get; }
        public string Description { get; }

        private PetRequisites(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Result<PetRequisites, Error> Create(string name, string description)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            if(string.IsNullOrWhiteSpace(description))
                return Errors.General.InvalidValue("Description");

            return new PetRequisites(name, description);
        }
    }
}