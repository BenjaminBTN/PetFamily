using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared.VO
{
    public record PhotosList
    {
        public IReadOnlyList<Photo> Photos { get; }


        private PhotosList(IEnumerable<Photo> photos)
        {
            Photos = photos.ToList();
        }


        public static Result<PhotosList, Error> Create(IEnumerable<Photo> photos)
        {
            if(photos == null)
                return Errors.General.NullValue("Photos");

            return new PhotosList(photos);
        }
    }
}