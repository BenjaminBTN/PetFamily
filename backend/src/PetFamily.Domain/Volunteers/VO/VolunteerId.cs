using System;

namespace PetFamily.Domain.Volunteers.VO
{
    public record VolunteerId
    {
        public Guid Value { get; }

        private VolunteerId(Guid value) => Value = value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VolunteerId" /> class.
        /// </summary>
        /// <returns>New <see cref="VolunteerId" /> with a uniq <see cref="Guid"/>.</returns>
        public static VolunteerId NewId() => new(Guid.NewGuid());

        public static VolunteerId Empty() => new(Guid.Empty);
        public static VolunteerId Create(Guid id) => new(id);
    }
}