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
    public class EfGayrimenkulDosya_TurDal : EfEntityRepositoryBase<GayrimenkulDosya_Tur, DtContext>, IGayrimenkulDosya_TurDal
    {
        public GayrimenkulDosya_Tur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.GayrimenkulDosya_Turleri.Include(gt => gt.Gayrimenkul).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public GayrimenkulDosya_Tur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.GayrimenkulDosya_Turleri.Include(gt => gt.Gayrimenkul).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.GayrimenkulDosya_Turleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.GayrimenkulDosya_Turleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<GayrimenkulDosya_Tur> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.GayrimenkulDosya_Turleri.Include(gt => gt.Gayrimenkul).ToList();
            }
        }
    }
}
