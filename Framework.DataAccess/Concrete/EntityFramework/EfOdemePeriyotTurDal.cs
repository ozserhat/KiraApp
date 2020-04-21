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
    public class EfOdemePeriyotTurDal : EfEntityRepositoryBase<OdemePeriyotTur, DtContext>, IOdemePeriyotTurDal
    {
        public OdemePeriyotTur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.OdemePeriyotTurleri.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public OdemePeriyotTur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.OdemePeriyotTurleri.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.OdemePeriyotTurleri.FirstOrDefault(i => i.Id == id && i.AktifMi == true);

                if (tur != null)
                {
                    context.OdemePeriyotTurleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<OdemePeriyotTur> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.OdemePeriyotTurleri.Where(a => a.AktifMi == true).ToList();
            }
        }
    }
}
