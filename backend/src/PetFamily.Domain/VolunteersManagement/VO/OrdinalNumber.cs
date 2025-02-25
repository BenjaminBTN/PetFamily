using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteersManagement.VO;

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


    internal Result<OrdinalNumber, Error> Forward() =>
        Create(Value + 1);

    internal Result<OrdinalNumber, Error> Back() =>
        Create(Value - 1);
}