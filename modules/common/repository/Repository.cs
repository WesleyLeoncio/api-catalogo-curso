using System.Linq.Expressions;
using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.common.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso.modules.common.repository;

public class Repository<T>: IRepository<T> where T : class
{
    private readonly AppDbConnectionContext _context;

    public Repository(AppDbConnectionContext context)
    {
        _context = context;
    }
    
    public IQueryable<T> GetIQueryable()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IEnumerable<T> GetAll()
    {
        return GetIQueryable().ToList();
    }
    
    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return GetIQueryable().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }
}