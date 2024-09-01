namespace Common.Exceptions;

public enum ErrorReason : byte
{
    ValidationRequired,
    ValidationNotInRange,
    ValidationNotComplex,
    ValidationGeneralError,
    ExistenceNotFound,
    ExistenceEmptyList,
    BusinessInsufficientBalance,
    GeneralError,
}

public sealed record InternalError(ErrorReason Reason, int StatusCode, string? Message = null);

public static class ValidationErrors
{
    public static readonly InternalError Required = new(ErrorReason.ValidationRequired, 400, "Data is required");

    public static readonly InternalError
        NotInRange = new(ErrorReason.ValidationNotInRange, 400, "Data is not in range");

    public static readonly InternalError NotComplex = new(ErrorReason.ValidationNotComplex, 400, "Data is not complex");

    public static InternalError General(string description = "Validations Error") =>
        new(ErrorReason.ValidationGeneralError, 400, description);
}

public static class ExistenceErrors
{
    public static InternalError NotFound(string description = "Data was not found") =>
        new(ErrorReason.ExistenceNotFound, 404, description);

    public static readonly InternalError EmptyList = new(ErrorReason.ExistenceEmptyList, 404, "Data List was empty");
}

public static class BusinessErrors
{
    public static readonly InternalError InsufficientBalance =
        new(ErrorReason.BusinessInsufficientBalance, 422, "Insufficient balance");
}

public static class GeneralErrors
{
    public static InternalError General(string description = "General Error") =>
        new(ErrorReason.GeneralError, 500, description);
}