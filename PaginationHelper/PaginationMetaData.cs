namespace ProductApi.PaginationHelper;

public class PaginationMetaData
{
    public int CurrentPage { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PaginationMetaData(int totalCount, int pageSize, int pageNumber)
    {
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

}