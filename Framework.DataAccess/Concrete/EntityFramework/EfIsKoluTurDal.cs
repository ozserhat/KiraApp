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
    public class EfIsKoluTurDal: EfEntityRepositoryBase<IsKoluTur, DtContext>, IIsKoluTurDAl
    {
        public IsKoluTur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.IsKoluTurleri.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public IsKoluTur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.IsKoluTurleri.Where(gt => gt.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.IsKoluTurleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.IsKoluTurleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
