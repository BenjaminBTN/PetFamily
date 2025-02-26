﻿using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared.VO;

public record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email, Error> Create(string value)
    {
        if(!Regex.IsMatch(value, @"^(\w+)@(\w+)\.(\w+)$"))
            return Errors.General.InvalidValue("Email");

        return new Email(value);
    }
}
