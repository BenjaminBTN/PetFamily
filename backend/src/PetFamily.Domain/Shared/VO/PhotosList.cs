using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared.VO
{
    public record PhotosList
    {
        public IReadOnlyList<Photo> Photos { get; } = [];

        private PhotosList() { }
        public PhotosList(IEnumerable<Photo> photos)
        {
            Photos = photos.ToList();
        }
    }
}
