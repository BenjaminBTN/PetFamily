using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Application.Volunteers.Update.SocialNetworks;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.VO;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public class UpdateSocialNetworksCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
    {
        public UpdateSocialNetworksCommandValidator()
        {
            RuleFor(c => c.VolunteerId).NotEmpty()
                .WithMessage(Errors.General.NullValue("{PropertyName}").Serialize());

            RuleForEach(c => c.SocialNetworkDtos).MustBeValueObject(r => VolunteerRequisite.Create(r.Name, r.Url));
        }
    }
}
