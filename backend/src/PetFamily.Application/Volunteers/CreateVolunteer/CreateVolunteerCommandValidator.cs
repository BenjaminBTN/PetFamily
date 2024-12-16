using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
    {
        public CreateVolunteerCommandValidator() 
        {
            RuleFor(c => c.FullNameDto).MustBeValueObject(f => FullName.Create(f.Name, f.Surname, f.Patronymic));

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());
            RuleFor(c => c.Description)
                .MaximumLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.Email).MustBeValueObject((value => Email.Create(value)));

            RuleFor(c => c.Experience)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.PhoneNumber).MustBeValueObject(value => PhoneNumber.Create(value)); 
        }
    }
}
