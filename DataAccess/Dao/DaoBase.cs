using DataAccess.Interface;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class DaoBase<T> : IDaoBase<T> where T : class, IEntity
    {
        protected ISession session;

        protected DaoBase()
            {
            session = NHibernateHelper.Session;
            }

        public Task<Tuple<IList<T>, int>> GetPagedEntitiesAsync(int startIndex, int requestedCount, string orderPropertyName = null, bool asc = true)
        {
            throw new NotImplementedException();
        }

        public virtual object Create(T entity)
        {
            object o;

            using(ITransaction transaction = session.BeginTransaction())
            {
               o = session.Save(entity);
                transaction.Commit();
            }
            return o;
        }

        public Task<object> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {

            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
            return session.QueryOver<T>().List<T>();
        }

        public Task<IList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {

            return session.CreateCriteria<T>().Add(Restrictions.Eq("Id", id)).UniqueResult<T>();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return session.CreateCriteria<T>().SetProjection(Projections.RowCount()).UniqueResult<int>();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public IList<T> GetLatestEntities(string datePropertyName, int requestedCount, out int totalCount)
        {
            totalCount = GetCount();
            return session.CreateCriteria<T>().AddOrder(Order.Desc(datePropertyName)).SetMaxResults(requestedCount)
                .List<T>();
        }

        public Task<Tuple<IList<T>, int>> GetLatestEntitiesAsync(string datePropertyName, int requestedCount)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {

            using (ITransaction transaction = session.BeginTransaction())
            {
               session.Update(entity);
               transaction.Commit();
            }
        }

        public virtual IList<T> GetPagedEntities(int startIndex, int requestedCount, out int totalCount,
            string orderColumn = null, bool asc = true)
        {
            ICriteria criteria = session.CreateCriteria<T>().SetFirstResult(startIndex).SetMaxResults(requestedCount);

            if (!string.IsNullOrEmpty(orderColumn))
            {
                criteria.AddOrder(asc ? Order.Asc(orderColumn) : Order.Desc(orderColumn));
            }

            totalCount = GetCount();
            return criteria.List<T>();
        }
    }
}
