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
    public class EfKira_DurumDal : EfEntityRepositoryBase<Kira_Durum, DtContext>, IKira_DurumDal
    {
        public Kira_Durum GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kira_Durumlari.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Kira_Durum GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kira_Durumlari.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Kira_Durumlari.FirstOrDefault(i => i.Id == id && i.AktifMi == true);

                if (tur != null)
                {
                    context.Kira_Durumlari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Kira_Durum> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Kira_Durumlari.Where(a => a.AktifMi == true).ToList();
            }
        }
    }
}
