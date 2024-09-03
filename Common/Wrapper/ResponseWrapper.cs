using Common.Exceptions;

namespace Common.Wrapper;

// Result Pattern
public class ResponseWrapper<T>
{
    public bool IsSuccessful { get; set; }

    public string Message { get; set; }

    public InternalError InternalError { get; set; }
    public T Data { get; set; }

    public ResponseWrapper()
    {
    }

    public ResponseWrapper(T data, string message = "Success")
    {
        IsSuccessful = true;
        Message = message;
        InternalError = default;
        Data = data;
    }

    public ResponseWrapper(InternalError internalError)
    {
        IsSuccessful = false;
        InternalError = internalError;
        Data = default;
    }

    public static ResponseWrapper<T> Success(T data, string message = "Success") => new(data, message);
    public static ResponseWrapper<T> Fail(InternalError internalError) => new(internalError);
}