using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IDaoBase<T> where T : class, IEntity
    {
        IList<T> GetAll();
        Task<IList<T>> GetAllAsync();
        IList<T> GetPagedEntities(int startIndex, int requestedCount, out int totalCount,
            string orderPropertyName = null, bool asc = true);
        Task<Tuple<IList<T>, int>> GetPagedEntitiesAsync(int startIndex, int requestedCount,
            string orderPropertyName = null, bool asc = true);

        object Create(T entity);
        Task<object> CreateAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        int GetCount();

        Task<int> GetCountAsync();

        IList<T> GetLatestEntities(string datePropertyName, int requestedCount, out int totalCount);
        Task<Tuple<IList<T>, int>> GetLatestEntitiesAsync(string datePropertyName, int requestedCount);

    }
}
