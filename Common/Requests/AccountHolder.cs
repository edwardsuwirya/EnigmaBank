namespace Common.Requests;

public record CreateAccountHolder(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string ContactNumber,
    string Email);

public record UpdateAccountHolder(
    int Id,
    string FirstName,
    string LastName,
    string ContactNumber,
    string Email);