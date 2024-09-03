using Common.Exceptions;
using FluentValidation;
using MediatR;
using Serilog;

namespace Application.Validations;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .Select(validationResult => validationResult)
            .ToList();

        if (errors.Count != 0)
        {
            var errMessage = string.Join("-", errors);

            Log.Error(errMessage);
            var appError = ValidationErrors.General(errMessage);

            return (TResponse)Activator.CreateInstance(typeof(TResponse),
                appError);
        }

        var response = await next();

        return response;
    }
}