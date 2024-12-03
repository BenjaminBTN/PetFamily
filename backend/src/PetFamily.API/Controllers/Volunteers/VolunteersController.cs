using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteers.Requests;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.Dtos;

namespace PetFamily.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices]CreateVolunteerHandler handler, 
            [FromBody]CreateVolunteerRequest request, 
            CancellationToken cancellationToken = default)
        {
            var fullName = new FullNameDto(request.Name, request.Surname, request.Patronymic);

            var command = new CreateVolunteerCommand(
                fullName,
                request.Description,
                request.Email,
                request.Experience,
                request.PhoneNumber);

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
