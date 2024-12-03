using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers.VO
{
    public record RequisitesForHelpList
    {
        public IReadOnlyList<RequisiteForHelp> Requisites { get; } = [];

        private RequisitesForHelpList() { }
        public RequisitesForHelpList(IEnumerable<RequisiteForHelp> requisites)
        {
            Requisites = requisites.ToList();
        }
    }
}
