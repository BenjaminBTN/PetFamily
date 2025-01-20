using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteers.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.AddFiles;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.DeleteFiles;
using PetFamily.Application.Volunteers.GetFiles;
using PetFamily.Application.Volunteers.HardDelete;
using PetFamily.Application.Volunteers.SoftDelete;
using PetFamily.Application.Volunteers.Update.MainInfo;
using PetFamily.Application.Volunteers.Update.Requsites;
using PetFamily.Application.Volunteers.Update.SocialNetworks;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.Volunteers
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
            [FromBody] UpdateMainInfoRequest request,
            [FromServices] UpdateMainInfoHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPut]
        [Route("{id:guid}/requsites")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateRequsitesRequest request,
            [FromServices] UpdateRequsitesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPut]
        [Route("{id:guid}/social-networks")]
        public async Task<ActionResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateSocialNetworksRequest request,
            [FromServices] UpdateSocialNetworksHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpDelete]
        [Route("{id:guid}/soft")]
        public async Task<ActionResult<Guid>> SoftDelete(
            [FromRoute] Guid id,
            [FromServices] SoftDeleteVolunteerHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new SoftDeleteVolunteerCommand(VolunteerId.Create(id));

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpDelete]
        [Route("{id:guid}/hard")]
        public async Task<ActionResult<Guid>> HardDelete(
            [FromRoute] Guid id,
            [FromServices] HardDeleteVolunteerHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new HardDeleteVolunteerCommand(VolunteerId.Create(id));

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPost]
        [Route("{id:guid}/pet")]
        public async Task<ActionResult> AddPet(
            IFormFile file,
            [FromRoute] string bucketName,
            [FromForm] AddPetRequest request,
            [FromServices] AddPetHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new AddPetCommand(request.Name);

            var result = await handler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPost]
        [Route("{id:guid}/pet/{petId:guid}")]
        public async Task<ActionResult<string>> AddPetPhotos(
            IFormFile file,
            [FromServices] AddFilesHandler handler,
            CancellationToken cancellationToken)
        {
            await using(var stream = file.OpenReadStream())
            {
                var extension = "." + file.FileName.Split('.').Last();
                var objectName = Guid.NewGuid().ToString() + extension;

                var command = new AddFilesCommand(stream, "photos", objectName);

                // validation

                var result = await handler.Handle(command, cancellationToken);
                if(result.IsFailure)
                    return result.Error.ToResponse();

                return new ObjectResult(Envelope.Ok(result.Value));
            }
        }

        [HttpGet]
        [Route("{volunteerId}/pet/{petId:guid}")]
        public async Task<ActionResult> GetPetPhotos(
            [FromQuery] GetFilesRequest request,
            [FromServices] GetFilesHandler handler,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            // validation

            var result = await handler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpDelete()]
        [Route("{volunteerId:guid}/pet/{petId:guid}")]
        public async Task<ActionResult> DeletePet(
            [FromQuery] DeleteFilesRequest request,
            [FromServices] DeleteFilesHandler handler,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            // validation

            var result = await handler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }
    }
}
