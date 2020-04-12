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
    public class EfGayrimenkulAlt_TurDal : EfEntityRepositoryBase<GayrimenkulAlt_Tur, DtContext>, IGayrimenkulAlt_TurDal
    {
        public GayrimenkulAlt_Tur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.GayrimenkulAlt_Turleri.Include(gt => gt.GayrimenkulTur).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public GayrimenkulAlt_Tur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.GayrimenkulAlt_Turleri.Include(gt => gt.GayrimenkulTur).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.GayrimenkulAlt_Turleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.GayrimenkulAlt_Turleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<GayrimenkulAlt_Tur> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.GayrimenkulAlt_Turleri.Include(gt => gt.GayrimenkulTur).ToList();
            }
        }
    }
}
