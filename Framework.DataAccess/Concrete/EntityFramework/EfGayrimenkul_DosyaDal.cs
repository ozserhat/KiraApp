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
    public class EfGayrimenkul_DosyaDal : EfEntityRepositoryBase<Gayrimenkul_Dosya, DtContext>, IGayrimenkul_DosyaDal
    {
        public Gayrimenkul_Dosya GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkul_Dosyalar.Include(gt => gt.GayrimenkulDosyaTurleri).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Gayrimenkul_Dosya GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkul_Dosyalar.Include(gt => gt.GayrimenkulDosyaTurleri).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Gayrimenkul_Dosyalar.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Gayrimenkul_Dosyalar.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Gayrimenkul_Dosya> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkul_Dosyalar.Include(gt => gt.GayrimenkulDosyaTurleri).ToList();
            }
        }

        public IEnumerable<Gayrimenkul_Dosya> GetirGayrimenkulId(int GayrimenkulId)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkul_Dosyalar.Include(gt => gt.GayrimenkulDosyaTurleri).Where(gta => gta.Gayrimenkul_Id == GayrimenkulId).ToList();
            }
        }
    }
}
