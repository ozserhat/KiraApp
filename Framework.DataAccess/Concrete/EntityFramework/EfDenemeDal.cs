using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfDenemeDal : EfEntityRepositoryBase<Deneme, DtContext>, IDenemeDal
    {
        public List<Deneme> GetirDenemeList()
        {
            using (DtContext context = new DtContext())
            {
                return context.Denemeler.ToList();
            }
        }
    }
}
