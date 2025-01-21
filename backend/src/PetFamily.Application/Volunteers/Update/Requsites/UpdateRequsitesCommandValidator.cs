﻿using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.Volunteers.Update.Requsites
{
    public class UpdateRequsitesCommandValidator : AbstractValidator<UpdateRequsitesCommand>
    {
        public UpdateRequsitesCommandValidator()
        {
            RuleFor(c => c.VolunteerId).NotEmpty()
                .WithMessage(Errors.General.NullValue("{PropertyName}").Serialize());

            RuleForEach(c => c.RequsiteDtos).MustBeValueObject(r => VolunteerRequisite.Create(r.Name, r.Description));
        }
    }
}
