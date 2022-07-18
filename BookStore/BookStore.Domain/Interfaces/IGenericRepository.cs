using System.Linq.Expressions;

namespace BookStore.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAll();

        Task Save();

    }
}
