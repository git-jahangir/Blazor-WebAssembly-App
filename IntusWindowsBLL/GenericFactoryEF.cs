using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IntusWindowsBLL
{
    public class GenericFactoryEF<C, T> : IGenericFactoryEF<T> where T : class where C : DbContext, new()
    {
        private C Context = new C();
        private DbSet<T> _dbset;

        protected C _dbctx
        {
            get { return Context; }
            set { Context = value; }
        }

        public GenericFactoryEF()
        {
            _dbset = _dbctx.Set<T>();
        }

        #region ===========readonly=========

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _dbset.ToListAsync();
        }

        public List<T> GetList()
        {
            return _dbset.ToList();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetBy(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.Where(predicate).ToListAsync();
        }

        public async Task<bool> HasData(Expression<Func<T, bool>> predicate)
        {
            return await FindBy(predicate) == null ? false : true;
        }
        #endregion

        #region ======crud operations=======
        public virtual void Insert(T entity)
        {
            _dbset.Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public virtual void InsertListAsync(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
        }

        public virtual void InsertListAsync(List<T> entities)
        {
            _dbset.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbctx.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbctx.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void DeleteListAsync(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }

        public virtual void DeleteListAsync(List<T> entities)
        {
            _dbset.RemoveRange(entities);
        }

        public virtual void DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> list = _dbset.Where(predicate);
            foreach (var entity in list)
            {
                _dbset.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _dbctx.SaveChangesAsync();
        }

        #endregion

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
                if (disposing)
                    _dbctx.Dispose();
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}