using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.VO
{
    public class Description
    {
        public string Value { get; }

        private Description(string value) => Value = value;

        public static Result<Description, Error> Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                return Errors.General.InvalidValue("Description");

            return new Description(value);
        }
    }
}
