using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.common.models;

namespace api_catalogo_curso.modules.categoria.service.interfaces;

public interface ICategoriaService
{
   CategoriaResponse Create(CategoriaRequest request);
   CategoriaResponse Update(int id, CategoriaRequest request);
   CategoriaResponse Delete(int id);
   CategoriaResponse GetId(int id);
   IEnumerable<CategoriaResponse> GetAll(int skip = 0, int take = 10);
   Pageable<CategoriaProdutoResponse> GetAllInclude(QueryParameters queryParameters);


}