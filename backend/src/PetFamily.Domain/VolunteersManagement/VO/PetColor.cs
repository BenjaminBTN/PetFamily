using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteersManagement.VO;

public record PetColor
{
    public string Value { get; }

    private PetColor(string value) => Value = value;

    public static Result<PetColor, Error> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            return Errors.General.InvalidValue("PetColor");

        if(value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.OverMaxLength("PetColor");

        return new PetColor(value);
    }
}
