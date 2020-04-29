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
    public class EfKiraciDal : EfEntityRepositoryBase<Kiraci, DtContext>, IKiraciDal
    {
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

        public Kiraci GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kiracilar.Where(a => a.Guid == guid).FirstOrDefault();
            }
        }

        public Kiraci GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kiracilar.Where(a => a.Id == id).FirstOrDefault();
            }
        }
    }
}
