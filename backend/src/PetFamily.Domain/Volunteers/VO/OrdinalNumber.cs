using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers.VO
{
    public record OrdinalNumber
    {
        public int Value { get; }

        private OrdinalNumber(int value)
        {
            Value = value;
        }

        public static Result<OrdinalNumber, Error> Create(int value)
        {
            if(value < 1)
                return Errors.General.InvalidValue("Ordinal number");

            return new OrdinalNumber(value);
        }
    }
}