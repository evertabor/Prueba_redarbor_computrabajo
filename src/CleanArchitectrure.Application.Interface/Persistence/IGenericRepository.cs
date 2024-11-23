using CleanArchitectrure.Domain.Entities;
using System.Linq.Expressions;

namespace CleanArchitectrure.Application.Interface.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        /* Commands */
        void Insert(T entity);
        void Update(T entity);
        void Delete(Expression<Func<T, bool>> where);

        /* Queries */
        Task<T?> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
