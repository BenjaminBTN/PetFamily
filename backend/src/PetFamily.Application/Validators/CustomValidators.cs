using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;
using System;

namespace PetFamily.Application.Validators;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder, 
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var result = factoryMethod(value);

            if(result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }

    public static IRuleBuilderOptionsConditions<T, TElement?> MustBeDate<T, TElement>(
        this IRuleBuilder<T, TElement?> ruleBuilder,
        Func<TElement, DateTime> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if(value == null)
                return;

            try
            {
                factoryMethod(value);
            }
            catch(Exception)
            {
                context.AddFailure(Errors.General.InvalidValue("Birth Date").Serialize());
            }
        });
    }
}
