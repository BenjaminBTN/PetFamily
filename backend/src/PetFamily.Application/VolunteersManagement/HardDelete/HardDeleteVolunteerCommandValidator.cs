using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteersManagement.HardDelete
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
