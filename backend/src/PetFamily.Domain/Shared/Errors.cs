using System;

namespace PetFamily.Domain.Shared
{
    public static class Errors
    {
        public static class General
        {
            public static Error InvalidValue(string? name = null)
            {
                var value = name ?? "Value";
                return Error.Validation("value.is.invalid", $"'{value}' is invalid");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = $" for ID '{id}'." ?? "";
                return Error.NotFound("record.is.not.found", $"Record is not found{forId}");
            }

            public static Error NullValue(string? name = null)
            {
                var value = name ?? "Value";
                return Error.Failure("value.can.not.be.null", $"'{value}' can not be null");
            }
        }
    }
}