using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;

namespace api_catalogo_curso.modules.categoria.service.interfaces;

public interface ICategoriaService
{
   CategoriaResponse Create(CategoriaRequest request);
   CategoriaResponse Update(int id, CategoriaRequest request);
   CategoriaResponse Delete(int id);
   CategoriaResponse GetId(int id);
   IEnumerable<CategoriaResponse> GetAll(int skip = 0, int take = 10);
   IEnumerable<CategoriaProdutoResponse> GetAllInclude(int skip = 0, int take = 10);


}