using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Extentions
{
    public static class ResponseExtentions
    {
        public static ActionResult ToResponse(this ErrorList errors)
        {
            if(errors.Any() == false)
            {
                return new ObjectResult(Envelope.Error(errors))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }


            var distinctErrorTypes = errors.Select(e => e.Type).Distinct().ToList();

            if(distinctErrorTypes.Count > 1)
            {
                return new ObjectResult(Envelope.Error(errors))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }


            var code = distinctErrorTypes.First() switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            return new ObjectResult(Envelope.Error(errors)) 
            { 
                StatusCode = code 
            };
        }
    }
}
