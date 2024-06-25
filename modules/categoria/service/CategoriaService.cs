using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.categoria.service.interfaces;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using AutoMapper;

namespace api_catalogo_curso.modules.categoria.service;

public class CategoriaService: ICategoriaService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uof;

    public CategoriaService(IMapper mapper, IUnitOfWork uof)
    {
        _mapper = mapper;
        _uof = uof;
    }

    public CategoriaResponse Create(CategoriaRequest request)
    {
        Categoria categoria = _uof.CategoriaRepository.Create(
            _mapper.Map<Categoria>(request));
        _uof.Commit();
        return _mapper.Map<CategoriaResponse>(categoria);
    }

    public CategoriaResponse Update(int id, CategoriaRequest request)
    {
        Categoria categoria = CheckCategory(id);
        _mapper.Map(request, categoria);
        Categoria update = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();
        return _mapper.Map<CategoriaResponse>(update);
    }

    public CategoriaResponse Delete(int id)
    {
        Categoria categoria = CheckCategory(id);
        _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();
        return _mapper.Map<CategoriaResponse>(categoria);
    }

    public CategoriaResponse GetId(int id)
    {
        Categoria categoria = CheckCategory(id);
        return (_mapper.Map<CategoriaResponse>(categoria));
    }

    public IEnumerable<CategoriaResponse> GetAll(int skip = 0, int take = 10)
    {
        return _mapper.Map<IEnumerable<CategoriaResponse>>(
            _uof.CategoriaRepository.GetAll(skip, take));
    }

    public IEnumerable<CategoriaProdutoResponse> GetAllInclude(int skip = 0, int take = 10)
    {
        return _mapper.Map<IEnumerable<CategoriaProdutoResponse>>(
            _uof.CategoriaRepository.GetAllInclude(skip, take));
    }

    private Categoria CheckCategory(int id)
    {
        return _uof.CategoriaRepository.Get(c => c.Id == id) ??
                 throw new NotFoundException("Categoria não encontrada!");
    }
    
}