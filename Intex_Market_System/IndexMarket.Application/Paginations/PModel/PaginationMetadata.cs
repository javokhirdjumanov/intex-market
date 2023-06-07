namespace IndexMarket.Application.Paginations;
public class PaginationMetadata
{
    public PaginationMetadata(
        int totalCount,
        int currentPage,
        int pageCount)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageCount = pageCount;
    }

    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }


    public bool HasNextPage
    {
        get
        {
            return CurrentPage < PageCount;
        }
    }
    public bool HasPreviousPage
    {
        get
        {
            return CurrentPage > 1 && CurrentPage <= PageCount + 1;
        }
    }
}
