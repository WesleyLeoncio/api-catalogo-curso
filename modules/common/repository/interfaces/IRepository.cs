using System.Linq.Expressions;

namespace api_catalogo_curso.modules.common.repository.interfaces;

public interface IRepository<T>
{
    protected IQueryable<T> GetIQueryable();
    IEnumerable<T> GetAll();
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}