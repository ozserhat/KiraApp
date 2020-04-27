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
    public class EfPersonelDal : EfEntityRepositoryBase<Personel, DtContext>, IPersonelDal
    {
        public List<Personel> GetirPersonelList()
        {
            using (DtContext context = new DtContext())
            {
                return context.Personeller.ToList();
            }
        }
    }
}
