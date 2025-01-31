using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.SpeciesManagement.VO
{
    public record SpeciesName
    {
        public string Value { get; }

        private SpeciesName(string value) => Value = value;

        public static Result<SpeciesName, Error> Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                return Errors.General.InvalidValue("SpeciesName");

            if(value.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.OverMaxLength("SpeciesName");

            return new SpeciesName(value);
        }
    }
}
