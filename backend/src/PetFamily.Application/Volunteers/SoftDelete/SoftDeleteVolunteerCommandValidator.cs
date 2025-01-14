﻿using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.SoftDelete
{
    public class SoftDeleteVolunteerCommandValidator : AbstractValidator<SoftDeleteVolunteerCommand>
    {
        public SoftDeleteVolunteerCommandValidator()
        {
            RuleFor(c => c.VolunteerId).NotEmpty()
                .WithMessage(Errors.General.NullValue("{PropertyName}").Serialize());
        }
    }
}
