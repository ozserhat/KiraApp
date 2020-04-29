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
    public class EfBeyanDosya_TurDal : EfEntityRepositoryBase<BeyanDosya_Tur, DtContext>, IBeyanDosya_TurDal
    {
        public BeyanDosya_Tur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.BeyanDosya_Turleri.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public BeyanDosya_Tur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.BeyanDosya_Turleri.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.BeyanDosya_Turleri.FirstOrDefault(i => i.Id == id && i.AktifMi == true);

                if (tur != null)
                {
                    context.BeyanDosya_Turleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<BeyanDosya_Tur> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.BeyanDosya_Turleri.Where(a => a.AktifMi == true).ToList();
            }
        }
    }
}
