using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Framework.Entities.Concrete;
using Framework.DataAccess.Abstract;
using Framework.Core.DataAccess.EntityFramework;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfAltGayrimenkul_KiraciDal : EfEntityRepositoryBase<AltGayrimenkul_Kiraci, DtContext>, IAltGayrimenkul_KiraciDal
    {
        public AltGayrimenkul_Kiraci GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.AltGayrimenkul_Kiraci
                              .Include(g => g.Gayrimenkul)
                              .Include(mh => mh.Mahalleler)
                              .Include(ilc => ilc.Mahalleler.Ilceler).Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public AltGayrimenkul_Kiraci GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.AltGayrimenkul_Kiraci
                              .Include(g => g.Gayrimenkul)
                              .Include(mh => mh.Mahalleler)
                              .Include(ilc => ilc.Mahalleler.Ilceler).Where(gt => gt.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.AltGayrimenkul_Kiraci.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.AltGayrimenkul_Kiraci.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }


        public IEnumerable<AltGayrimenkul_Kiraci> GetirListe(int gayrimenkulId)
        {
            using (DtContext context = new DtContext())
            {
                return context.AltGayrimenkul_Kiraci
                              .Include(kt => kt.KiraciTurleri)
                              .Include(g => g.Gayrimenkul)
                              .Include(mh => mh.Mahalleler)
                              .Include(ilc => ilc.Mahalleler.Ilceler).Where(gt => gt.Gayrimenkul_Id == gayrimenkulId).ToList();
            }
        }

        public IEnumerable<AltGayrimenkul_Kiraci> GetirListeAktif(int gayrimenkulId)
        {
            using (DtContext context = new DtContext())
            {
                return context.AltGayrimenkul_Kiraci
                    .Include(kt => kt.KiraciTurleri)
                              .Include(g => g.Gayrimenkul)
                              .Include(mh => mh.Mahalleler)
                              .Include(ilc => ilc.Mahalleler.Ilceler)
                              .Where(gt => gt.AktifMi == true && gt.Gayrimenkul_Id == gayrimenkulId).ToList();
            }
        }

        public bool Ekle(AltGayrimenkul_Kiraci entities)
        {
            int sonuc = 0;

            using (DtContext context = new DtContext())
            {
                context.AltGayrimenkul_Kiraci.Add(entities);

                sonuc = context.SaveChanges();
            }

            if (sonuc > 0)
                return true;

            return false;
        }

        public bool Guncelle(IEnumerable<AltGayrimenkul_Kiraci> entities)
        {
            int sonuc = 0;

            using (DtContext context = new DtContext())
            {
                context.AltGayrimenkul_Kiraci.AddRange(entities);

                sonuc = context.SaveChanges();
            }

            if (sonuc > 0)
                return true;

            return false;
        }
    }
}
