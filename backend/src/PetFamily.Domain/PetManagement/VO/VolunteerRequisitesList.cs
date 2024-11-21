using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.PetManagement.VO
{
    public record VolunteerRequisitesList
    {
        public IReadOnlyList<VolunteerRequisite> Requisites { get; }


        private VolunteerRequisitesList(IEnumerable<VolunteerRequisite> requisites)
        {
            Requisites = requisites.ToList();
        }


        public static Result<VolunteerRequisitesList, Error> Create(IEnumerable<VolunteerRequisite> requisites)
        {
            if(requisites == null)
                return Errors.General.NullValue("Requisites");

            return new VolunteerRequisitesList(requisites);
        }
    }
}