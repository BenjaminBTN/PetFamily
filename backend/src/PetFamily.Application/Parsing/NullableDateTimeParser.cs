using System;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Extensions;

public static class NullableDateTimeParser
{
    public static Result<DateTime?, Error> CreateDateTime(string? value)
    {
        DateTime? dateTime;
        if (value == null)
            dateTime = null;
        else
        {
            try
            {
                dateTime = DateTime.Parse(value).ToLocalTime();
            }
            catch (Exception)
            {
                return Errors.General.InvalidValue(nameof(value));
            }
        }

        return dateTime;
    }
}
