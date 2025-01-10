using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.VO
{
    public record Description
    {
        public string Value { get; }

        private Description(string value) => Value = value;

        public static Result<Description, Error> Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                return Errors.General.InvalidValue("Description");

            if(value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
                return Errors.General.OverMaxLength("Description");

            return new Description(value);
        }
    }
}
