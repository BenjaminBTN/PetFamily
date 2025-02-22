using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.SpeciesManagement.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.SpeciesManagement.AddBreed;
using PetFamily.Application.SpeciesManagement.Create;
using PetFamily.Application.SpeciesManagement.DeleteBreed;
using PetFamily.Application.SpeciesManagement.DeleteSpecies;

namespace PetFamily.API.Controllers.SpeciesManagement;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateSpeciesRequest request,
        [FromServices] CreateSpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Envelope.Ok(result.Value);
    }

    [HttpPost]
    [Route("{id:guid}/breed")]
    public async Task<ActionResult<Guid>> AddBreed(
        [FromRoute] Guid id,
        [FromForm] AddBreedRequest request,
        [FromServices] AddBreedHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Envelope.Ok(result.Value);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteSpeciesHandler handler,
        CancellationToken ct = default)
    {
        var command = new DeleteSpeciesCommand(id);

        var result = await handler.Handle(command, ct);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Envelope.Ok();
    }

    [HttpDelete]
    [Route("{speciesId:guid}/{breedId:guid}")]
    public async Task<ActionResult> Delete(
    [FromRoute] Guid speciesId,
    [FromRoute] Guid breedId,
    [FromServices] DeleteBreedHandler handler,
    CancellationToken ct = default)
    {
        var command = new DeleteBreedCommand(speciesId, breedId);

        var result = await handler.Handle(command, ct);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Envelope.Ok();
    }
}
