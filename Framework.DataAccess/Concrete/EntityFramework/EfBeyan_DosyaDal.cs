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
    public class EfBeyan_DosyaDal : EfEntityRepositoryBase<Beyan_Dosya, DtContext>, IBeyan_DosyaDal
    {
        public Beyan_Dosya GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Beyan_Dosya GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Beyan_Dosyalari.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Beyan_Dosyalari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Beyan_Dosya> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).ToList();
            }
        }
    }
}

