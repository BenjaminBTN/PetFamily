using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Volunteers.VO
{
    public record RequisiteForHelpList
    {
        public IReadOnlyList<RequisiteForHelp> Requisites { get; } = new List<RequisiteForHelp>();

        public RequisiteForHelpList() { }
        public RequisiteForHelpList(IEnumerable<RequisiteForHelp> requisites)
        {
            Requisites = requisites.ToList();
        }
    }
}
