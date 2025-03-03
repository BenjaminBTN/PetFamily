using System;
using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.UpdatePet;

public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.PetId)
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

        RuleForEach(c => c.RequisitesForHelp)
            .MustBeValueObject(r => RequisiteForHelp.Create(r.Name, r.Description));
    }
}
