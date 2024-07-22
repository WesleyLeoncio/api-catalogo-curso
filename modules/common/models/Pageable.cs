namespace api_catalogo_curso.modules.common.models;

public record Pageable<T>(
    List<T> Content,
    int CurrentPage,
    int TotalPages,
    int PageSize,
    int TotalCount,
    bool HasPrevious,
    bool HasNext
);