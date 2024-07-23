using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.pagination.models.response;

namespace api_catalogo_curso.modules.categoria.service.interfaces;

public interface ICategoriaService
{
   CategoriaResponse Create(CategoriaRequest request);
   CategoriaResponse Update(int id, CategoriaRequest request);
   CategoriaResponse Delete(int id);
   CategoriaResponse GetId(int id);
   IEnumerable<CategoriaResponse> GetAll();
   PageableResponse<CategoriaProdutoResponse> GetAllIncludePageable(QueryParameters queryParameters);
   PageableResponse<CategoriaResponse> GetAllFilterPageable(CategoriaFiltroRequest filtroRequest);


}