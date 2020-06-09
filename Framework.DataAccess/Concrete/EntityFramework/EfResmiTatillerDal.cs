using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Framework.Entities.Concrete;
using Framework.DataAccess.Abstract;
using Framework.Core.DataAccess.EntityFramework;


namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfResmiTatillerDal : EfEntityRepositoryBase<ResmiTatiller, DtContext>, IResmiTatillerDal
    {
        public ResmiTatiller GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.ResmiTatiller.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.ResmiTatiller.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.ResmiTatiller.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
