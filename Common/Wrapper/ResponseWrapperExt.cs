using Common.Enums;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Common.Wrapper;

public static class ResponseWrapperExt
{
    public static IActionResult Match<T>(this ResponseWrapper<T> wrapper, Func<IActionResult> onSuccess,
        AppError onFailure)
    {
        if (wrapper.IsSuccessful)
        {
            return onSuccess();
        }

        var message = onFailure.Description;
        throw onFailure.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundException(message),
            ErrorType.EmptyList => new NotFoundException(message),
            ErrorType.General => new GeneralException(message),
            ErrorType.Required => new BadRequestException(message),
            ErrorType.NotInRange => new BadRequestException(message),
            ErrorType.NotComplex => new BadRequestException(message),
            _ => new GeneralException(message)
        };
    }
}