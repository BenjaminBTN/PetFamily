using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers.VO
{
    public record RequisiteForHelpList
    {
        public IReadOnlyList<RequisiteForHelp> Requisites { get; } = [];

        private RequisiteForHelpList() { }
        public RequisiteForHelpList(IEnumerable<RequisiteForHelp> requisites)
        {
            Requisites = requisites.ToList();
        }
    }
}
