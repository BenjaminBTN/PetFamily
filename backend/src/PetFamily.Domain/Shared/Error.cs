using System;

namespace PetFamily.Domain.Shared
{
    public record Error
    {
        private const string SEPARATOR = "||";

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? InvalidValue { get; }

        private Error(string code, string message, ErrorType type, string? invalidValue = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidValue = invalidValue;
        }

        public static Error Validation(string code, string message, string? invalidValue = null) =>
            new Error(code, message, ErrorType.Validation, invalidValue);

        public static Error NotFound(string code, string message) =>
            new Error(code, message, ErrorType.NotFound);

        public static Error Failure(string code, string message) =>
            new Error(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message) =>
            new Error(code, message, ErrorType.Conflict);

        // вместо метода расширения
        public ErrorList ToErrorList()
        {
            return new ErrorList([this]);
        }


        public string Serialize()
        {
            return string.Join(SEPARATOR, Code, Message, Type.ToString());
        }


        public static Error Deserialise(string serialised)
        {
            var parts = serialised.Split(SEPARATOR);

            if(parts.Length < 3 || parts.Length > 3)
                throw new ArgumentOutOfRangeException("Substrings Count");

            if(Enum.TryParse<ErrorType>(parts[2], out var type) == false)
                throw new ArgumentException("Invalid serialized format (Error Type)");

            return new Error(parts[0], parts[1], type);
        }
    }


    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}
