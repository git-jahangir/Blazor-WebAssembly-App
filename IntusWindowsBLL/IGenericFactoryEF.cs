using System.Linq.Expressions;

namespace IntusWindowsBLL
{
    public interface IGenericFactoryEF<T> : IDisposable where T : class
    {        
        Task<IEnumerable<T>> GetAllAsync();
        Task<List<T>> GetListAsync();
        List<T> GetList();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<bool> HasData(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void InsertListAsync(IEnumerable<T> entity);
        void InsertListAsync(List<T> entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void DeleteListAsync(List<T> entity);
        void DeleteListAsync(IEnumerable<T> entity);
        void DeleteAsync(Expression<Func<T, bool>> predicate);
        Task SaveAsync();
    }
}