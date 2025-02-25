using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteersManagement.Commands.AddPetPhotos;

public class AddPetPhotosCommandValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotosCommandValidator()
    {
        RuleFor(c => c.VolunteerId)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.PetId)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.BucketName)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize())
            .MaximumLength(Constants.MAX_LOW_TEXT_LENGTH)
            .WithMessage(Errors.General.OverMaxLength("{PropertyName}").Serialize());
    }
}
