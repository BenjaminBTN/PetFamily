using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.VolunteersManagement.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Processors;
using PetFamily.API.Response;
using PetFamily.Application.VolunteersManagement.Commands.AddPet;
using PetFamily.Application.VolunteersManagement.Commands.AddPetPhotos;
using PetFamily.Application.VolunteersManagement.Commands.Create;
using PetFamily.Application.VolunteersManagement.Commands.DeleteFiles;
using PetFamily.Application.VolunteersManagement.Commands.GetFiles;
using PetFamily.Application.VolunteersManagement.Commands.HardDelete;
using PetFamily.Application.VolunteersManagement.Commands.MovePet;
using PetFamily.Application.VolunteersManagement.Commands.SoftDelete;
using PetFamily.Application.VolunteersManagement.Commands.Update.MainInfo;
using PetFamily.Application.VolunteersManagement.Commands.Update.Requsites;
using PetFamily.Application.VolunteersManagement.Commands.Update.SocialNetworks;
using PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination;

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

            if (result.IsFailure)
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

            if (result.IsFailure)
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

            if (result.IsFailure)
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

            if (result.IsFailure)
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
            var command = new SoftDeleteVolunteerCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
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
            var command = new HardDeleteVolunteerCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPost]
        [Route("{id:guid}/pets")]
        public async Task<ActionResult> AddPet(
            [FromRoute] Guid id,
            [FromBody] AddPetRequest request,
            [FromServices] AddPetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPost]
        [Route("{volunteerId:guid}/pets/{petId:guid}")]
        public async Task<ActionResult<string>> AddPetPhotos(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromQuery] string bucketName,
            IFormFileCollection files,
            [FromServices] AddPetPhotosHandler handler,
            CancellationToken cancellationToken = default)
        {
            await using var processor = new FileProcessor();
            var filesDtoResult = processor.Process(files, cancellationToken);
            if (filesDtoResult.IsFailure)
                return filesDtoResult.Error.ToErrorList().ToResponse();

            var command = new AddPetPhotosCommand(volunteerId, petId, filesDtoResult.Value, bucketName);

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpGet]
        [Route("{volunteerId}/pets/{petId:guid}")]
        public async Task<ActionResult> GetPetPhotos(
            [FromQuery] GetFilesRequest request,
            [FromServices] GetFilesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpDelete]
        [Route("{volunteerId:guid}/pets/{petId:guid}")]
        public async Task<ActionResult> DeletePetPhotos(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromQuery] DeletePetPhotosRequest request,
            [FromServices] DeleteFilesHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(volunteerId, petId);

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpPut]
        [Route("{volunteerId:guid}/pets/")]
        public async Task<ActionResult> MovePet(
            [FromRoute] Guid volunteerId,
            [FromQuery] MovePetRequest request,
            [FromServices] MovePetHandler handler,
            CancellationToken ct = default)
        {
            var command = request.ToCommand(volunteerId);

            var result = await handler.Handle(command, ct);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(Envelope.Ok(result.Value));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVolunteers(
            [FromQuery] GetAllVolunteersWithPaginationRequest request,
            [FromServices] GetAllVolunteersWithPaginationHandler handler,
            CancellationToken ct)
        {
            var query = request.ToQuery();

            var result = await handler.Handle(query, ct);

            return new ObjectResult(result);
        }
    }
}
