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
    public class EfKiraciTurDal : EfEntityRepositoryBase<KiraciTur, DtContext>, IKiraciTurDal
    {
        public KiraciTur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraciTurleri.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public KiraciTur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraciTurleri.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.KiraciTurleri.FirstOrDefault(i => i.Id == id && i.AktifMi == true);

                if (tur != null)
                {
                    context.KiraciTurleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<KiraciTur> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraciTurleri.Where(a => a.AktifMi == true).ToList();
            }
        }
    }
}
