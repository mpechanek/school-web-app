using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class VstupenkaDao : DaoBase<Vstupenka>
    {
        public VstupenkaDao() : base()
        {

        }
        public IList<Vstupenka> GetVstupenkyPaged(int count, int page, out int totalVstupenky)
        {
            totalVstupenky = session.CreateCriteria<Vstupenka>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<Vstupenka>()
                .AddOrder(Order.Asc("Id"))
                .SetFirstResult((page - 1) * count).
                SetMaxResults(count).
                List<Vstupenka>();
        }
        public IList<Vstupenka> SearchVstupenka(string phrase)
        {
            return session.CreateCriteria<Vstupenka>()
                .Add(Restrictions.Like(nameof(Vstupenka.Jmeno), string.Format("%{0}%", phrase)))
                .List<Vstupenka>();
        }
    }
}
