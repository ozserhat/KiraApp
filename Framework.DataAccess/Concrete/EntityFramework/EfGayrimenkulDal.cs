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
    public class EfGayrimenkulDal:EfEntityRepositoryBase<Gayrimenkul, DtContext>, IGayrimenkulDal
    {
        public Gayrimenkul GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkuller.Include(gt => gt.GayrimenkulTur).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Gayrimenkul GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkuller.Include(gt => gt.GayrimenkulTur).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Gayrimenkuller.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Gayrimenkuller.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Gayrimenkul> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkuller.Include(gt => gt.GayrimenkulTur).ToList();
            }
        }
    }
}
