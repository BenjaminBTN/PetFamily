using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteSpecies;

public class DeleteSpeciesCommandValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());
    }
}
