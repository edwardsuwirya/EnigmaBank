using Common.Exceptions;

namespace Common.Wrapper;

// Result Pattern
public class ResponseWrapper<T>
{
    public bool IsSuccessful { get; set; }

    public string Message { get; set; }

    public InternalError InternalError { get; set; }
    public T Data { get; set; }

    public ResponseWrapper<T> Success(T data, string message = "Success")
    {
        IsSuccessful = true;
        Message = message;
        InternalError = default;
        Data = data;

        return this;
    }

    public ResponseWrapper<T> Fail(InternalError internalError)
    {
        IsSuccessful = false;
        InternalError = internalError;
        Data = default;
        return this;
    }
}