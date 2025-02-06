using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement.VO;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.MovePet
{
    public class MovePetHandler
    {
        private readonly IVolunteersRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IValidator<MovePetCommand> _validator;
        private readonly ILogger<MovePetHandler> _logger;

        public MovePetHandler(
            IVolunteersRepository repository, 
            IUnitOfWork unitOfWork, 
            //IValidator<MovePetCommand> validator, 
            ILogger<MovePetHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            //_validator = validator;
            _logger = logger;
        }

        public async Task<Result<int, ErrorList>> Handle(MovePetCommand command, CancellationToken ct)
        {
            // validation

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

            var currentPosition = pet.OrdinalNumber.Value;

            var newOrdinalNumberResult = OrdinalNumber.Create(command.NewPosition);
            if(newOrdinalNumberResult.IsFailure)
                return newOrdinalNumberResult.Error.ToErrorList();
            var newOrdinalNumber = newOrdinalNumberResult.Value;

            var movePetResult = volunteer.MovePet(pet, newOrdinalNumber);
            if(movePetResult.IsFailure)
                return movePetResult.Error.ToErrorList();

            var result = _unitOfWork.SaveChanges(ct);

            _logger.LogInformation(
                "Pet ordinal number successfully changed from {current} to {new}", 
                currentPosition, pet.OrdinalNumber.Value);

            return pet.OrdinalNumber.Value;
        }
    }
}
