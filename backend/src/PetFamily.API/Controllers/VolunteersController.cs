using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Volunteers.CreateVolunteer;

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
            var result = await handler.Handle(request, cancellationToken);

            if(result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}