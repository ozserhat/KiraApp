using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Framework.Entities.Concrete;
using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfKira_BeyanDal : EfEntityRepositoryBase<Kira_Beyan, DtContext>, IKira_BeyanDal
    {
        public Kira_Beyan GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kira_Beyanlari.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Kira_Beyanlari.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Kira_Beyanlari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
