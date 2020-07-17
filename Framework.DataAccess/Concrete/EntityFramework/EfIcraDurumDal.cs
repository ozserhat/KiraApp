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
    public class EfIcraDurumDal : EfEntityRepositoryBase<IcraDurum, DtContext>, IIcraDurumDal
    {
        public IcraDurum GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.IcraDurumlari.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.IcraDurumlari.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.IcraDurumlari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
