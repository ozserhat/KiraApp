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
    public class EfKiraDurum_DosyaTurDal : EfEntityRepositoryBase<KiraDurum_DosyaTur, DtContext>, IKiraDurum_DosyaTurDal
    {
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var dosya = context.KiraDurum_DosyaTurleri.FirstOrDefault(i => i.Id == id && i.AktifMi == true);

                if (dosya != null)
                {
                    context.KiraDurum_DosyaTurleri.Remove(dosya);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public KiraDurum_DosyaTur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraDurum_DosyaTurleri.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public KiraDurum_DosyaTur GetirDosyaBeyanTur(int beyanDosyaTurId)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraDurum_DosyaTurleri.Where(gta => gta.BeyanDosyaTur_Id == beyanDosyaTurId).FirstOrDefault();
            }
        }

        public IEnumerable<KiraDurum_DosyaTur> GetirDosyaBeyanTurListe(int beyanDosyaTurId)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraDurum_DosyaTurleri.Where(gta => gta.BeyanDosyaTur_Id == beyanDosyaTurId).ToList();
            }
        }

        public KiraDurum_DosyaTur GetirKiraDurum(int kiraDurumId)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraDurum_DosyaTurleri.Where(gta => gta.KiraDurum_Id == kiraDurumId).FirstOrDefault();
            }
        }

        public IEnumerable<KiraDurum_DosyaTur> GetirKiraDurumListe(int kiraDurumId)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraDurum_DosyaTurleri
                              .Include(kd => kd.Kira_Durumlari)
                              .Include(dt => dt.BeyanDosya_Turleri).Where(gta => gta.KiraDurum_Id == kiraDurumId).ToList();
            }
        }

        public IEnumerable<KiraDurum_DosyaTur> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraDurum_DosyaTurleri
                              .Include(kd=>kd.Kira_Durumlari)
                              .Include(dt=>dt.BeyanDosya_Turleri)
                              .Where(gta => gta.BeyanDosya_Turleri.KapatmaMi.Value).ToList();
            }
        }
    }
}
