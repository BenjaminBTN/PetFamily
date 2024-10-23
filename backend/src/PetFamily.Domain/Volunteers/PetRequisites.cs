using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
{
    public record PetRequisites
    {
        private PetRequisites(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }

        public static Result<PetRequisites> Create(string name, string description)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result.Failure<PetRequisites>("Name can not be empty");

            if(string.IsNullOrWhiteSpace(description))
                return Result.Failure<PetRequisites>("Description can not be empty");

            return new PetRequisites(name, description);
        }
    }
}
