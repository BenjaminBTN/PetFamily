using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
{
    public class VolunteerRequisite
    {
        private VolunteerRequisite(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }

        public static Result<VolunteerRequisite> Create(string name, string description)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result.Failure<VolunteerRequisite>("Name can not be empty");

            if(string.IsNullOrWhiteSpace(description))
                return Result.Failure<VolunteerRequisite>("Description can not be empty");

            return new VolunteerRequisite(name, description);
        }
    }
}
