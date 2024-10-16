﻿namespace PetFamily.Domain.Volunteers
{
    public record PetId
    {
        public Guid Value { get; }

        private PetId(Guid value)
        {
            Value = value;
        }

        public static PetId NewVolonteerId() => new(Guid.NewGuid());

        public static PetId Empty() => new(Guid.Empty);

        public static PetId Create(Guid id) => new(id);
    }
}