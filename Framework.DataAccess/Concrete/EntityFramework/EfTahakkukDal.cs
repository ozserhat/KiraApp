using System;
using System.Data.Entity;
using System.Linq;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using Framework.Core.DataAccess.EntityFramework;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfTahakkukDal : EfEntityRepositoryBase<Tahakkuk, DtContext>, ITahakkukDal
    {
        public bool Add(IEnumerable<Tahakkuk> entities)
        {
            int sonuc = 0;

            using (DtContext context = new DtContext())
            {
                context.Tahakkuklar.AddRange(entities);

                sonuc = context.SaveChanges();
            }

            if (sonuc > 0)
                return true;

            return false;
        }
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tahakkuk = context.Tahakkuklar.FirstOrDefault(i => i.Id == id);

                if (tahakkuk != null)
                {
                    context.Tahakkuklar.Remove(tahakkuk);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public Tahakkuk GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar.Include(kb => kb.Kira_Beyani).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public Tahakkuk GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar.Include(kb => kb.Kira_Beyani).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Tahakkuk GetirBeyan(int BeyanId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar.Include(kb => kb.Kira_Beyani)
                       .Where(gta => gta.Kira_Beyani.Beyan_Id == BeyanId).FirstOrDefault();
            }
        }

        public Tahakkuk GetirGayrimenkul(int GayrimenkulId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar
                    .Include(kb => kb.Kira_Beyani)
                    .Include(kb => kb.Kira_Beyani.Beyanlar)
                    .Include(kb => kb.Kira_Beyani.Gayrimenkuller)
                    .Where(gta => gta.Kira_Beyani.Gayrimenkul_Id == GayrimenkulId).FirstOrDefault();
            }
        }

        public Tahakkuk GetirKiraci(int KiraciId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar
                     .Include(kb => kb.Kira_Beyani)
                     .Include(kb => kb.Kira_Beyani.Beyanlar)
                     .Include(kb => kb.Kira_Beyani.Gayrimenkuller)
                     .Where(gta => gta.Kira_Beyani.Kiraci_Id == KiraciId).FirstOrDefault();
            }
        }

        public IEnumerable<Tahakkuk> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar.Include(kb => kb.Kira_Beyani).Include(b=>b.Kira_Beyani.Beyanlar).ToList();
            }
        }

        public List<Tahakkuk> GetirListeBeyanId(int KiraBeyanId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Tahakkuklar
                     .Include(kb => kb.Kira_Beyani)
                     .Where(a=>a.KiraBeyan_Id==KiraBeyanId)
                     .ToList();
            }
        }

    }
}
