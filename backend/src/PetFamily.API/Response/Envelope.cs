using Microsoft.AspNetCore.Mvc;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Response
{
    public record Envelope
    {
        public object? Result { get; }
        public ErrorList? Errors { get; }
        public DateTime TimeGenerated { get; }

        private Envelope(object? result, ErrorList? errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.Now.ToLocalTime();
        }

        public static ObjectResult Ok(object? result = null)
        {
            return new(new Envelope(result, null));
        }

        public static Envelope Error(ErrorList? errors = null)
        {
            return new(null, errors);
        }
    }
}
