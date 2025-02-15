using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.GetFiles
{
    public class GetFilesCommandValidator : AbstractValidator<GetFilesCommand>
    {
        public GetFilesCommandValidator()
        {
            RuleFor(c => c.ObjectName)
                .MustBeValueObject(FilePath.Create);

            RuleFor(c => c.BucketName)
                .NotEmpty()
                .WithMessage(Errors.General.InvalidValue("{PropertyName}").Serialize())
                .MaximumLength(Constants.MAX_LOW_TEXT_LENGTH)
                .WithMessage(Errors.General.OverMaxLength("{PropertyName}").Serialize());
        }
    }
}
