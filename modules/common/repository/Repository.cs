using System.Linq.Expressions;
using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.common.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso.modules.common.repository;

public class Repository<T>: IRepository<T> where T : class
{

    protected readonly AppDbConnectionContext Context;

    public Repository(AppDbConnectionContext context)
    {
        Context = context;
    }


    public IEnumerable<T> GetAll(int skip = 0, int take = 10)
    {
        return Context.Set<T>().AsNoTracking().Skip(skip).Take(take).ToList();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return Context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        Context.Set<T>().Add(entity);
        Context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        Context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        Context.SaveChanges();
        return entity;
    }
}