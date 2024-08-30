using Common.Enums;

namespace Common.Exceptions;

public record AppError(ErrorType ErrorType, string Description)
{
    public static class Messages
    {
        public const string EmptyList = "No data was not found";
        public const string NotFound = "Data with Id '{0}' was not found";
        public const string FieldRequired = "Field '{0}' is required";
        public const string GeneralException = "General Error";
    }

    public static AppError EmptyList() => new(
        ErrorType.EmptyList, Messages.EmptyList);

    public static AppError NotFound(int id) => new(
        ErrorType.NotFound, string.Format(Messages.NotFound, id));

    public static AppError Validations(string message) => new(
        ErrorType.Validations, message);

    public static AppError GeneralError() => new(
        ErrorType.General, Messages.GeneralException);
}