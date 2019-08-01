using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class HokejistiUserDao : DaoBase<HokejistiUser>
    {
        public HokejistiUser GetByLoginAndPassword(string login, string password)
        {
            return session.CreateCriteria<HokejistiUser>()
                .Add(Restrictions.Eq("Login", login))
                .Add(Restrictions.Eq("Password", password))
                .UniqueResult<HokejistiUser>();
        }


        public HokejistiUser GetByLogin(string login)
        {
            return session.CreateCriteria<HokejistiUser>()
                .Add(Restrictions.Eq("Login", login))
                .UniqueResult<HokejistiUser>();
        }
        public IList<HokejistiUser> GetUsersInCategoryId(int id)
        {
            return session.CreateCriteria<HokejistiUser>()
                .CreateAlias("Role", "rol")
                .Add(Restrictions.Eq("rol.Id", id))
                .List<HokejistiUser>();
        }

        public IList<HokejistiUser> GetUsersPaged(int count, int page, out int totalUsers)
        {
            totalUsers = session.CreateCriteria<HokejistiUser>().SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<HokejistiUser>()
                .AddOrder(Order.Asc("Name"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<HokejistiUser>();
        }

        public IList<HokejistiUser> GetUsersByCategoryPaged(int id, int count, int page, out int totalUsers)
        {
            totalUsers = session.CreateCriteria<HokejistiUser>()
                .CreateAlias("Role", "rol")
                .Add(Restrictions.Eq("rol.Id", id))
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<HokejistiUser>()
                .CreateAlias("Role", "rol")
                .Add(Restrictions.Eq("rol.Id", id))
                .AddOrder(Order.Asc("Name"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<HokejistiUser>();
        }

        public IList<HokejistiUser> SearchUser(string phrase)
        {
            return session.CreateCriteria<HokejistiUser>()
                .Add(Restrictions.Like("Login", string.Format("%{0}%", phrase)))
                .List<HokejistiUser>();
        }

        public IList<HokejistiUser> GetAdmins()
        {
            return session.CreateCriteria<HokejistiUser>()
                .CreateAlias("Role", "rol")
                .Add(Restrictions.Eq("rol.Id", 1))
                .List<HokejistiUser>();
        }

        public IList<HokejistiUser> GetUsers()
        {
            return session.CreateCriteria<HokejistiUser>()
                .CreateAlias("Role", "rol")
                .Add(Restrictions.Eq("rol.Id", 2))
                .List<HokejistiUser>();
        }
    }

}

