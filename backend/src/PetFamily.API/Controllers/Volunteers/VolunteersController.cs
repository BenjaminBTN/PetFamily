using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteers.Requests;
using PetFamily.API.Extentions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.Create;

namespace PetFamily.API.Controllers
{
    public class VolunteersController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateVolunteerRequest request,
            [FromServices] CreateVolunteerHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPut]
        [Route("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerRequest request,
            [FromServices] UpdateMainInfoHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }
    }
}
