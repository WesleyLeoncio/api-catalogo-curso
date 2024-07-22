namespace api_catalogo_curso.modules.common.models;

public class Pageable<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<T> Content { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }

    public Pageable(int currentPage, int totalPages, int pageSize, int totalCount, List<T> content, bool hasPrevious, bool hasNext)
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