using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.Update.MainInfo
{
    public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
    {
        public UpdateMainInfoCommandValidator()
        {
            RuleFor(c => c.VolunteerId)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.FullNameDto)
                .MustBeValueObject(f => FullName.Create(f.Name, f.Surname, f.Patronymic));

            RuleFor(c => c.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(c => c.Email)
                .MustBeValueObject(Email.Create);

            RuleFor(c => c.Experience)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleFor(c => c.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);
        }
    }
}
