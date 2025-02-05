using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;
using System;

namespace PetFamily.Application.VolunteersManagement.AddPet
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.Name)
                .MustBeValueObject(PetName.Create);

            RuleFor(c => c.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(c => c.TypeInfo.Species)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize())
                .MaximumLength(Constants.MAX_LOW_TEXT_LENGTH)
                .WithMessage(Errors.General.OverMaxLength("{PropertyName}").Serialize());

            RuleFor(c => c.TypeInfo.Breed)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize())
                .MaximumLength(Constants.MAX_LOW_TEXT_LENGTH)
                .WithMessage(Errors.General.OverMaxLength("{PropertyName}").Serialize());

            RuleFor(c => c.Color)
                .MustBeValueObject(PetColor.Create);

            RuleFor(c => c.HealthInfo)
                .MustBeValueObject(PetHealthInfo.Create);

            RuleFor(c => c.Address)
                .MustBeValueObject(a => Address.Create(
                a.Country, a.Region, a.City, a.Street, a.HouseNumber, a.PostalCode));

            RuleFor(c => c.Weight)
                .GreaterThan(0)
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.Height)
                .GreaterThan(0)
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);

            RuleFor(c => c.BirthDate)
                .MustBeDate(DateTime.Parse);

            RuleFor(c => c.Status)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize())
                .LessThanOrEqualTo(2)
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());
        }
    }
}
