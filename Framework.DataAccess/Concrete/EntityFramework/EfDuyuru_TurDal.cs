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
    public class EfDuyuru_TurDal : EfEntityRepositoryBase<Duyuru_Tur, DtContext>, IDuyuru_TurDal
    {
        public Duyuru_Tur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyuru_Turleri.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Duyuru_Tur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyuru_Turleri.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Duyuru_Turleri.FirstOrDefault(i => i.Id == id && i.AktifMi == true);

                if (tur != null)
                {
                    context.Duyuru_Turleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Duyuru_Tur> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyuru_Turleri.Where(a => a.AktifMi == true).ToList();
            }
        }
    }
}
