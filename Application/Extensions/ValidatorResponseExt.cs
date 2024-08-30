using Common.Enums;
using Common.Exceptions;
using Common.Wrapper;
using FluentValidation.Results;

namespace Application.Extensions;

public static class ValidatorResponseExt
{
    public static ResponseWrapper<T> ValidationResponse<T>(this ValidationResult validationResult)
    {
        if (validationResult.IsValid) return null;
        var appError = AppError.Validations(validationResult.ToString("~"));
        return new ResponseWrapper<T>().Fail(appError);
    }
}