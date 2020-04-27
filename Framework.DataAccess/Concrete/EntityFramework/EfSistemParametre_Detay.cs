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
    public class EfSistemParametre_Detay : EfEntityRepositoryBase<SistemParametre_Detay, DtContext>, ISistemParametre_DetayDal
    {
        public SistemParametre_Detay GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.SistemParametre_Detaylari.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public SistemParametre_Detay GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.SistemParametre_Detaylari.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.SistemParametreleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.SistemParametreleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<SistemParametre_Detay> GetirListe(int? parametreId)
        {
            using (DtContext context = new DtContext())
            {
                return parametreId == null
                      ? context.SistemParametre_Detaylari.ToList()
                      : context.SistemParametre_Detaylari.Include(sp => sp.SistemParametreleri).Where(a => a.SistemParametre_Id == parametreId).ToList();

            }
        }
    }
}
