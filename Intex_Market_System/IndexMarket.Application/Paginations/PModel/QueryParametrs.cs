namespace IndexMarket.Application.Paginations;

public class QueryParametrs
{
    public PaginationParam Page { get; set; }
}
public class PaginationParam
{
    public int Size { get; set; } = 10;
    public int Index { get; set; } = 1;
}
