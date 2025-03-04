using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteersManagement.Commands.HardDeletePet;

public class HardDeletePetCommandValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetCommandValidator()
    {
        RuleFor(c => c.VolunteerId)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.PetId)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());
    }
}
