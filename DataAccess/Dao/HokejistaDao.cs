using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace DataAccess.Dao
{
    public class HokejistaDao : DaoBase<Hokejista>
    {
        public HokejistaDao() : base()
        {

        }

        public IList<Hokejista> GetHokejistiPaged(int count, int page, out int totalPlayers)
        {
            totalPlayers = session.CreateCriteria<Hokejista>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<Hokejista>()
                .AddOrder(Order.Desc("PocetBodu"))
                .SetFirstResult((page - 1) * count).
                SetMaxResults(count).
                List<Hokejista>();
        }

        public IList<Hokejista> SearchPlayers(string phrase)
        {
            return session.CreateCriteria<Hokejista>()
                .Add(Restrictions.Like(nameof(Hokejista.Jmeno), string.Format("%{0}%", phrase)))
                .List<Hokejista>();
        }
        
        public IList<Hokejista> GetPlayersInLeagueId(int id)
        {
            return session.CreateCriteria<Hokejista>()
                .CreateAlias("Liga", "liga")
                .Add(Restrictions.Eq("liga.Id", id)).AddOrder(Order.Desc("PocetBodu"))
                .List<Hokejista>();
        }

        public IList<Hokejista> GetPlayersWithPost(int id)
        {
            return session.CreateCriteria<Hokejista>()
                .CreateAlias("Post", "post")
                .Add(Restrictions.Eq("post.Id", id)).AddOrder(Order.Desc("PocetBodu"))
                .List<Hokejista>();
        }

        public virtual IList<Hokejista> GetSortedEntities(
            string orderColumn = null, bool asc = true)
        {
            ICriteria criteria = session.CreateCriteria<Hokejista>();

            if (!string.IsNullOrEmpty(orderColumn))
            {
                 criteria = session.CreateCriteria<Hokejista>().AddOrder(asc ? Order.Asc(orderColumn) : Order.Desc(orderColumn));
            }

           
            return criteria.List<Hokejista>();
        }
    }
}
