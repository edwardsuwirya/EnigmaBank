namespace Common.Exceptions;

public class GeneralException(string message) : Exception(message);

public class NotFoundException(string message) : Exception(message);

public class BadRequestException(string message) : Exception(message);