using Common.Exceptions;
using Common.Wrapper;
using FluentValidation.Results;

namespace Application.Extensions;

public static class ValidatorResponseExt
{
    public static ResponseWrapper<T> ValidationResponse<T>(this ValidationResult validationResult)
    {
        return validationResult.IsValid
            ? null
            : ResponseWrapper<T>.Fail(ValidationErrors.General(validationResult.ToString("~")));
    }
}