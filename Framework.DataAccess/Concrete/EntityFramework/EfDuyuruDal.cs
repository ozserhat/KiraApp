using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Framework.Entities.Concrete;
using Framework.DataAccess.Abstract;
using Framework.Core.DataAccess.EntityFramework;
using System.Linq.Expressions;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfDuyuruDal : EfEntityRepositoryBase<Duyuru, DtContext>, IDuyuruDal
    {
        public Duyuru GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyurular.Include(x => x.Duyuru_Turleri).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Duyuru GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyurular.Include(x => x.Duyuru_Turleri).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Duyurular.FirstOrDefault(i => i.Id == id && i.AktifMi == true);

                if (tur != null)
                {
                    context.Duyurular.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Duyuru> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyurular.Include(x => x.Duyuru_Turleri).ToList();
            }
        }
    }
}
