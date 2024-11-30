using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers.VO
{
    public record VolunteerRequisitesList
    {
        public IReadOnlyList<VolunteerRequisite> Requisites { get; }

        private VolunteerRequisitesList() { }
        private VolunteerRequisitesList(List<VolunteerRequisite> requisites)
        {
            Requisites = requisites;
        }


        public static Result<VolunteerRequisitesList, Error> Create(IEnumerable<VolunteerRequisite> requisites)
        {
            if(requisites == null)
                return Errors.General.NullValue("Requisites");

            return new VolunteerRequisitesList(requisites.ToList());
        }
    }
}