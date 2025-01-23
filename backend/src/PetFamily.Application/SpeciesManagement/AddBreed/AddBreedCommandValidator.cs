using FluentValidation;
using PetFamily.Application.SpeciesManagement.AddBreed;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.VO;

namespace PetFamily.Application.SpeciesManagement.Create
{
    public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
    {
        public AddBreedCommandValidator()
        {
            RuleFor(c => c.SpeciesId).NotEmpty()
                .WithMessage(Errors.General.NullValue("{PropertyName}").Serialize());

            RuleFor(c => c.Name).MustBeValueObject(value => BreedName.Create(value));
        }
    }
}
