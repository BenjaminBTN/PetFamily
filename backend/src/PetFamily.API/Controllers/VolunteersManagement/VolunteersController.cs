using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.VolunteersManagement.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Processors;
using PetFamily.API.Response;
using PetFamily.Application.VolunteersManagement.AddFiles;
using PetFamily.Application.VolunteersManagement.AddPet;
using PetFamily.Application.VolunteersManagement.Create;
using PetFamily.Application.VolunteersManagement.DeleteFiles;
using PetFamily.Application.VolunteersManagement.GetFiles;
using PetFamily.Application.VolunteersManagement.HardDelete;
using PetFamily.Application.VolunteersManagement.SoftDelete;
using PetFamily.Application.VolunteersManagement.Update.MainInfo;
using PetFamily.Application.VolunteersManagement.Update.Requsites;
using PetFamily.Application.VolunteersManagement.Update.SocialNetworks;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.API.Controllers.VolunteersManagement
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
            var command = request.ToCommand(id); // to refact command

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
            var command = request.ToCommand(id); // to refact command

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
            var command = request.ToCommand(id); // to refact command

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
            var command = new SoftDeleteVolunteerCommand(VolunteerId.Create(id)); // to refact command

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
            var command = new HardDeleteVolunteerCommand(VolunteerId.Create(id)); // to refact command

            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPost]
        [Route("{id:guid}/pet")]
        public async Task<ActionResult> AddPet(
            [FromRoute] Guid id,
            [FromBody] AddPetRequest request,
            [FromServices] AddPetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPost]
        [Route("{volunteerId:guid}/pet/{petId:guid}")]
        public async Task<ActionResult<string>> AddPetPhotos(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromQuery] string bucketName,
            IFormFileCollection files,
            [FromServices] AddFilesHandler handler,
            CancellationToken cancellationToken = default)
        {
            await using var processor = new FileProcessor();
            var filesDto = processor.Process(files, cancellationToken);

            var command = new AddFilesCommand(volunteerId, petId, filesDto, bucketName);

            var result = await handler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpGet]
        [Route("{volunteerId}/pet/{petId:guid}")]
        public async Task<ActionResult> GetPetPhotos(
            [FromQuery] GetFilesRequest request,
            [FromServices] GetFilesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpDelete()]
        [Route("{volunteerId:guid}/pet/{petId:guid}")]
        public async Task<ActionResult> DeletePetPhotos(
            [FromQuery] DeleteFilesRequest request,
            [FromServices] DeleteFilesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }
    }
}
