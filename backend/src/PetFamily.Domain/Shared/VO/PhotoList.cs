﻿using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace PetFamily.Domain.Shared.VO
{
    public record PhotoList
    {
        public IReadOnlyList<Photo> Photos { get; } = [];

        private PhotoList() { }
        public PhotoList(IEnumerable<Photo> photos)
        {
            Photos = photos.ToList();
        }
    }
}
