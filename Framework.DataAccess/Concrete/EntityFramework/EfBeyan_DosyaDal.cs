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
        public Beyan_Dosya GetById(int id, bool? kapatmaMi)
        {
            using (DtContext context = new DtContext())
            {
                if (kapatmaMi.HasValue && kapatmaMi.Value)
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Id == id && gta.BeyanDosyaTurleri.KapatmaMi.Value).FirstOrDefault();
                else
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Id == id && !gta.BeyanDosyaTurleri.KapatmaMi.Value).FirstOrDefault();
            }
        }

        public Beyan_Dosya GetByGuid(Guid guid, bool? kapatmaMi)
        {
            using (DtContext context = new DtContext())
            {
                if (kapatmaMi.HasValue && kapatmaMi.Value)
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Guid == guid && gta.BeyanDosyaTurleri.KapatmaMi.Value).FirstOrDefault();
                else
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Guid == guid && !gta.BeyanDosyaTurleri.KapatmaMi.Value).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Beyan_Dosyalari.FirstOrDefault(i => i.Id == id && !i.BeyanDosyaTurleri.KapatmaMi.Value);

                if (tur != null)
                {
                    context.Beyan_Dosyalari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Beyan_Dosya> GetirListe(bool? kapatmaMi)
        {
            using (DtContext context = new DtContext())
            {
                if (kapatmaMi.HasValue && kapatmaMi.Value)
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.BeyanDosyaTurleri.KapatmaMi.Value).ToList();
                else
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => !gta.BeyanDosyaTurleri.KapatmaMi.Value).ToList();
            }
        }

        public IEnumerable<Beyan_Dosya> GetirBeyanId(int BeyanId, bool? kapatmaMi)
        {
            using (DtContext context = new DtContext())
            {
                if (kapatmaMi.HasValue && kapatmaMi.Value)
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Beyan_Id == BeyanId && gta.BeyanDosyaTurleri.KapatmaMi.Value).ToList();
                else
                    return context.Beyan_Dosyalari.Include(gt => gt.BeyanDosyaTurleri).Where(gta => gta.Beyan_Id == BeyanId && !gta.BeyanDosyaTurleri.KapatmaMi.Value).ToList();
            }
        }
    }
}

