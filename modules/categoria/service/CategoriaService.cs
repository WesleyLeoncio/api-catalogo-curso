using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using AutoMapper;
using X.PagedList;

namespace api_catalogo_curso.modules.categoria.service;
//TODO REFATORAR CLASS E CRIAR UMA INTERFACE
public class CategoriaService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    
    public CategoriaService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<CategoriaResponse> CadastroDeCategoria(CategoriaRequest request)
    {
        Categoria newCategoria = 
            _uof.CategoriaRepository.Create(_mapper.Map<Categoria>(request));
        await _uof.Commit();
        return _mapper.Map<CategoriaResponse>(newCategoria);
    }
    
    public async Task<IEnumerable<CategoriaResponse>> ListarCategorias()
    {
        IEnumerable<Categoria> categorias = await _uof.CategoriaRepository.GetAllAsync();
       return _mapper.Map<IEnumerable<CategoriaResponse>>(categorias);
    }
    
    public async Task<IPagedList<Categoria>> GetAllIncludePageable(QueryParameters queryParameters)
    {
        return await _uof.CategoriaRepository.GetAllIncludePageableAsync(queryParameters);
    }

    public IEnumerable<CategoriaProdutoResponse> ResposeGetAllIncludePageable(IPagedList<Categoria> categorias)
    {
        return _mapper.Map<IEnumerable<CategoriaProdutoResponse>>(categorias);
    }

    public async Task<CategoriaResponse> BuscarCategoria(int id)
    {
        return _mapper.Map<CategoriaResponse>(await CheckCategoria(id));
    }
    
    private async Task<Categoria> CheckCategoria(int id)
    {
        return await _uof.CategoriaRepository.GetAsync(c => c.Id == id)??
               throw new NotFoundException("Categoria não encontrada!");
    }
}