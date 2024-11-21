using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.PetManagement.VO
{
    public record RequisitesForHelpList
    {
        public IReadOnlyList<RequisiteForHelp> Requisites { get; }


        private RequisitesForHelpList(IEnumerable<RequisiteForHelp> requisites)
        {
            Requisites = requisites.ToList();
        }


        public static Result<RequisitesForHelpList, Error> Create(IEnumerable<RequisiteForHelp> requisites)
        {
            if(requisites == null)
                return Errors.General.NullValue("Requisites");

            return new RequisitesForHelpList(requisites);
        }
    }
}