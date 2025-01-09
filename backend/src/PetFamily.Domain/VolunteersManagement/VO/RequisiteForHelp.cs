using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteersManagement.VO
{
    public record RequisiteForHelp
    {
        public string Name { get; }
        public string Description { get; }

        private RequisiteForHelp(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Result<RequisiteForHelp, Error> Create(string name, string description)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            if(string.IsNullOrWhiteSpace(description))
                return Errors.General.InvalidValue("Description");

            return new RequisiteForHelp(name, description);
        }
    }
}