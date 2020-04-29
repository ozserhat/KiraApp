using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Framework.DataAccess.Concrete.EntityFramework
{
   public class EfKiraciDal : EfEntityRepositoryBase<Kiraci, DtContext>, IKiraciDal
    {
        public Kiraci GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kiracilar.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }
         
        public Kiraci GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kiracilar.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Kiracilar.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Kiracilar.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Kiraci> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Kiracilar.ToList();
            }
        }
    }
}
