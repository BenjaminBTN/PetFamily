using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers.VO
{
    public record VolunteerRequisitesList
    {
        public IReadOnlyList<VolunteerRequisite> Requisites { get; } = [];

        public VolunteerRequisitesList() { }
        public VolunteerRequisitesList(IEnumerable<VolunteerRequisite> requisites)
        {
            Requisites = requisites.ToList();
        }
    }
}