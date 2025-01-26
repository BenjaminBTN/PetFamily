using FluentValidation;
using PetFamily.Application.SpeciesManagement.AddBreed;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.VO;
using System;

namespace PetFamily.Application.SpeciesManagement.Create
{
    public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
    {
        public AddBreedCommandValidator()
        {
            RuleFor(c => c.SpeciesId)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.Name).MustBeValueObject(BreedName.Create);
        }
    }
}
