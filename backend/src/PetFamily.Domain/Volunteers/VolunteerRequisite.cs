using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers
{
    public class VolunteerRequisite
    {
        public string Name { get; }
        public string Description { get; }

        private VolunteerRequisite(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Result<VolunteerRequisite, Error> Create(string name, string description)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Errors.General.InvalidValue("Name");

            if(string.IsNullOrWhiteSpace(description))
                return Errors.General.InvalidValue("Description");

            return new VolunteerRequisite(name, description);
        }
    }
}