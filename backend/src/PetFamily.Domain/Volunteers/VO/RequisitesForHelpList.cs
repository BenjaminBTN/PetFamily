using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers.VO
{
    public record RequisitesForHelpList
    {
        public IReadOnlyList<RequisiteForHelp> Requisites { get; }

        private RequisitesForHelpList() { }
        private RequisitesForHelpList(List<RequisiteForHelp> requisites)
        {
            Requisites = requisites;
        }


        public static Result<RequisitesForHelpList, Error> Create(IEnumerable<RequisiteForHelp> requisites)
        {
            if(requisites == null)
                return Errors.General.NullValue("Requisites");

            return new RequisitesForHelpList(requisites.ToList());
        }
    }
}