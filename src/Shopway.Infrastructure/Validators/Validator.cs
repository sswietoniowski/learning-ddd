﻿using Microsoft.IdentityModel.Tokens;
using Shopway.Application.Abstractions;
using Shopway.Domain.BaseTypes;
using Shopway.Domain.Errors;
using Shopway.Domain.Results;

namespace Shopway.Infrastructure.Validators;

public sealed class Validator : IValidator
{
    private readonly List<Error> _errors;

    public Validator()
    {
        _errors = new();
    }

    public bool IsValid => _errors.IsNullOrEmpty();
    public bool IsInvalid => IsValid is false;

    /// <summary>
    /// If the provided condition is true, then error will be added to the error list
    /// </summary>
    /// <param name="condition">Condition for invalid state</param>
    /// <param name="thenError">Error that will be added to the error list if condition is true</param>
    /// <returns>IValidator to chain validation</returns>
    public IValidator If(bool condition, Error thenError)
    {
        if (condition is true)
        {
            _errors.Add(thenError);
        }

        return this;
    }

    /// <summary>
    /// If the result is failure, then result error will be added to the error list
    /// </summary>
    /// <typeparam name="TValueObject">ValueObject type</typeparam>
    /// <param name="resultOfValueObject">Result containing the value object</param>
    /// <returns>IValidator to chain validation</returns>
    public IValidator Validate<TValueObject>(Result<TValueObject> resultOfValueObject)
        where TValueObject : ValueObject
    {
        if (resultOfValueObject.IsFailure)
        {
            _errors.Add(resultOfValueObject.Error);
        }

        return this;
    }

    /// <summary>
    /// If the validation result is failure, then all validation result errors will be added to the error list
    /// </summary>
    /// <typeparam name="TValueObject">ValueObject type</typeparam>
    /// <param name="valueObject">ValidationResult containing the value object</param>
    /// <returns>IValidator to chain validation</returns>
    public IValidator Validate<TValueObject>(ValidationResult<TValueObject> valueObject)
        where TValueObject : ValueObject
    {
        if (valueObject.IsFailure)
        {
            _errors.AddRange(valueObject.ValidationErrors);
        }

        return this;
    }

    /// <summary>
    /// Builds the failure validation result with errors
    /// </summary>
    /// <typeparam name="TResponse">Type of response</typeparam>
    /// <returns>ValidationResult with errors</returns>
    /// <exception cref="InvalidOperationException">If validator error list is null or empty, throw exception</exception>
    public ValidationResult<TResponse> Failure<TResponse>()
        where TResponse : IResponse
    {
        if (IsValid)
        {
            throw new InvalidOperationException("Validation was successful, but Failure was called");
        }

        return ValidationResult<TResponse>.WithErrors(_errors.ToArray());
    }
}