using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.SpeciesManagement.VO
{
    public record BreedName
    {
        public string Value { get; }

        private BreedName(string value) => Value = value;

        public static Result<BreedName, Error> Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                return Errors.General.InvalidValue("BreedName");

            if(value.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.OverMaxLength("BreedName");

            return new BreedName(value);
        }
    }
}
