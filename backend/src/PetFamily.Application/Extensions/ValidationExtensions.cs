using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using System.Collections.Generic;

namespace PetFamily.Application.Extensions;

public static class ValidationExtensions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult, 
        ILogger logger, 
        string action, 
        string propertyName)
    {
        var validationErrors = validationResult.Errors;
        List<Error> errors = [];

        foreach(var validationError in validationErrors)
        {
            var error = Error.Deserialise(validationError.ErrorMessage);
            errors.Add(Error.Validation(error.Code, error.Message, validationError.PropertyName));

            logger.LogError(
                "Can not {action} the {propertyName} record due to a validation error: {errorMessage}", 
                action, propertyName, error.Message);
        }

        return new ErrorList(errors);
    }
}
