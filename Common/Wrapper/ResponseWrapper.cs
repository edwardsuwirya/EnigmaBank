using Common.Exceptions;

namespace Common.Wrapper;

// Result Pattern
public class ResponseWrapper<T>
{
    public bool IsSuccessful { get; set; }

    public List<string> Messages { get; set; }

    public AppError Error { get; set; }
    public T Data { get; set; }

    public ResponseWrapper<T> Success(T data, string message = null)
    {
        IsSuccessful = true;
        Messages = [message];
        Data = data;

        return this;
    }

    public ResponseWrapper<T> Fail(AppError error)
    {
        IsSuccessful = false;
        Error = error;
        return this;
    }
}