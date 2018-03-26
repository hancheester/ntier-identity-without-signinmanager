using System.Linq;

namespace web_service.Data
{
    public interface IRepository<T>
    {
        T Return(object id);
        int Create(T entity);
        IQueryable<T> Table { get; }
    }
}