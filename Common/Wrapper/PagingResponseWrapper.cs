namespace Common.Wrapper;

public class PagingResponseWrapper<T> : ResponseWrapper<T>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int PageCount { get; set; }
    public int TotalItem { get; set; }

    private PagingResponseWrapper(PageWrapper<T> data, string message = null)
    {
        Page = data.Page;
        PerPage = data.PageSize;
        PageCount = data.TotalPages;
        TotalItem = data.TotalItems;
        IsSuccessful = true;
        Message = message;
        Data = data.Items;
    }

    public static PagingResponseWrapper<T> Success(PageWrapper<T> data, string message = null) => new(data, message);
}