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
    public class EfGayrimenkulTurDal : EfEntityRepositoryBase<GayrimenkulTur, DtContext>, IGayrimenkulTurDal
    {
        public GayrimenkulTur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.GayrimenkulTurleri.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public GayrimenkulTur GetByGuid(Guid guid)
        {
            using (DtContext context=new DtContext())
            {
                return context.GayrimenkulTurleri.Where(gt=>gt.Guid==guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.GayrimenkulTurleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.GayrimenkulTurleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
