using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteersManagement.Commands.SelectMainPetPhoto;

public class SelectMainPetPhotoCommandValidator : AbstractValidator<SelectMainPetPhotoCommand>
{
    public SelectMainPetPhotoCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.PetId)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());

        RuleFor(c => c.FilePath)
            .NotEmpty()
            .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize());
    }
}