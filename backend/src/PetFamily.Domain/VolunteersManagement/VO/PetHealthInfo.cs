using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteersManagement.VO;

public record PetHealthInfo
{
    public string Value { get; }

    private PetHealthInfo(string value) => Value = value;

    public static Result<PetHealthInfo, Error> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            return Errors.General.InvalidValue("PetHealthInfo");

        if(value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
            return Errors.General.OverMaxLength("PetHealthInfo");

        return new PetHealthInfo(value);
    }
}
