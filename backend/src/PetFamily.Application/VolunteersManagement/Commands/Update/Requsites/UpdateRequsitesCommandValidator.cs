using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.Update.Requsites
{
    public class UpdateRequsitesCommandValidator : AbstractValidator<UpdateRequsitesCommand>
    {
        public UpdateRequsitesCommandValidator()
        {
            RuleFor(c => c.VolunteerId)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

            RuleForEach(c => c.RequsitesDto)
                .MustBeValueObject(r => VolunteerRequisite.Create(r.Name, r.Description));
        }
    }
}
