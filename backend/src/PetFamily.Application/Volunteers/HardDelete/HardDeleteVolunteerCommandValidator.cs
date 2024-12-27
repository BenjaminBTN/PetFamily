using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.HardDelete
{
    public class HardDeleteVolunteerCommandValidator : AbstractValidator<HardDeleteVolunteerCommand>
    {
        public HardDeleteVolunteerCommandValidator()
        {
            RuleFor(c => c.VolunteerId).NotEmpty()
                .WithMessage(Errors.General.NullValue("{PropertyName}").Serialize());
        }
    }
}
