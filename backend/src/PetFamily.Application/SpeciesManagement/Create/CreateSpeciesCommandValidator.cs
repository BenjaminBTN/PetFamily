using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.SpeciesManagement.VO;

namespace PetFamily.Application.SpeciesManagement.Create
{
    public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
    {
        public CreateSpeciesCommandValidator()
        {
            RuleFor(c => c.Name).MustBeValueObject(value => SpeciesName.Create(value));
        }
    }
}
