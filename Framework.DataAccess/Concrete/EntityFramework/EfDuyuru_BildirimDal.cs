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
using Framework.DataAccess.Concrete.EntityFramework;

namespace Framework.DataAccess.Concrete
{
    public class EfDuyuru_BildirimDal : EfEntityRepositoryBase<Duyuru_Bildirim, DtContext>, IDuyuru_BildirimDal
    {
        public Duyuru_Bildirim GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyuru_Bildirimleri.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Duyuru_Bildirimleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Duyuru_Bildirimleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Duyuru_Bildirim> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Duyuru_Bildirimleri.ToList();
            }
        }

        public bool Add(IEnumerable<Duyuru_Bildirim> entities)
        {
            int sonuc = 0;

            using (DtContext context = new DtContext())
            {
                context.Duyuru_Bildirimleri.AddRange(entities);

                sonuc = context.SaveChanges();
            }

            if (sonuc > 0)
                return true;

            return false;
        }
    }
}
