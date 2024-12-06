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
            RuleFor(c => c.FullName).MustBeValueObject((f => FullName.Create(f.Name, f.Surname, f.Patronymic)));
            RuleFor(c => c.Description).NotEmpty().MaximumLength(Constants.MAX_HIGH_TEXT_LENGTH);
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Experience).GreaterThanOrEqualTo(0);
            RuleFor(c => c.PhoneNumber).Matches(@"^\+?[1-9][0-9]{10}$");
        }
    }
}
