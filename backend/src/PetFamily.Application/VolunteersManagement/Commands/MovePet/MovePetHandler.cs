using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Commands.MovePet
{
    public class MovePetHandler : ICommandHandler<int, MovePetCommand>
    {
        private readonly IVolunteersRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<MovePetCommand> _validator;
        private readonly ILogger<MovePetHandler> _logger;

        public MovePetHandler(
            IVolunteersRepository repository,
            IUnitOfWork unitOfWork,
            IValidator<MovePetCommand> validator,
            ILogger<MovePetHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<int, ErrorList>> Handle(MovePetCommand command, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);
            if(validationResult.IsValid == false)
                return validationResult.ToErrorList(_logger, "update", "pet");

            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteerResult = await _repository.GetById(volunteerId, ct);
            if(volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();
            var volunteer = volunteerResult.Value;

            var petId = PetId.Create(command.PetId);
            var petToMoveResult = volunteer.GetPetById(petId);
            if(petToMoveResult.IsFailure)
                return petToMoveResult.Error.ToErrorList();
            var pet = petToMoveResult.Value;

            var currentOrdinalNumber = pet.OrdinalNumber.Value;
            var newOrdinalNumber = OrdinalNumber.Create(command.NewPosition).Value;

            var movePetResult = volunteer.MovePet(pet, newOrdinalNumber);
            if(movePetResult.IsFailure)
                return movePetResult.Error.ToErrorList();

            var result = _unitOfWork.SaveChanges(ct);

            _logger.LogInformation(
                "Pet ordinal number successfully changed from {current} to {new}",
                currentOrdinalNumber, pet.OrdinalNumber.Value);

            return pet.OrdinalNumber.Value;
        }
    }
}
