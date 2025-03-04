using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteersManagement.Commands.ChangePetStatus;

public class ChangePetStatusCommandValidator : AbstractValidator<ChangePetStatusCommand>
{
    public ChangePetStatusCommandValidator()
    {
        RuleFor(c => c.VolunteerId)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.PetId)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.Status)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize())
            .GreaterThanOrEqualTo(0)
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize())
            .LessThanOrEqualTo(2)
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());
    }
}
