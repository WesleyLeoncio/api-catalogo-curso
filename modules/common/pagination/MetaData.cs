using X.PagedList;

namespace api_catalogo_curso.modules.common.pagination;

public class MetaData<T>
{
    public int Count { get; private set; }
    public int PageSize { get; private set; }
    public int PageCount { get; private set; }
    public int TotalItemCount { get; private set; }
    public bool HasNextPage { get; private set; }
    public bool HasPreviousPage { get; private set; }

    public MetaData(int count, int pageSize, int pageCount, int totalItemCount, bool hasNextPage, bool hasPreviousPage)
    {
        Count = count;
        PageSize = pageSize;
        PageCount = pageCount;
        TotalItemCount = totalItemCount;
        HasNextPage = hasNextPage;
        HasPreviousPage = hasPreviousPage;
    }

    public static MetaData<T> ToValue(IPagedList<T> page)
    {
        return new MetaData<T>(
            page.Count,
            page.PageSize,
            page.PageCount,
            page.TotalItemCount,
            page.HasNextPage,
            page.HasPreviousPage
        );
    }
}