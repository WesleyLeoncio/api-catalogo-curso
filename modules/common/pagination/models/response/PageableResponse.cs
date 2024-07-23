namespace api_catalogo_curso.modules.common.pagination.models.response;

public class PageableResponse<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<T> Content { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }

    public PageableResponse(int currentPage, int totalPages, int pageSize, int totalCount, List<T> content, bool hasPrevious, bool hasNext)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
        Content = content;
        HasPrevious = hasPrevious;
        HasNext = hasNext;
    }
}