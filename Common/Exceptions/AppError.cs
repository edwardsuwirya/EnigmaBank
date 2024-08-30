using Common.Enums;

namespace Common.Exceptions;

public record AppError(ErrorType ErrorType, string Description)
{
    public static AppError EmptyList() => new(
        ErrorType.EmptyList, $"No data was not found");

    public static AppError NotFound(int id) => new(
        ErrorType.NotFound, $"Data with Id '{id}' was not found");

    public static AppError FieldIsRequired(string field) => new(
        ErrorType.Required, $"'{field}' is required");

    public static AppError GeneralError() => new(
        ErrorType.General, $"General Error");
}