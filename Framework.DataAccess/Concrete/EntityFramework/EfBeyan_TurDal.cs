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

    public class EfBeyan_TurDal : EfEntityRepositoryBase<Beyan_Tur, DtContext>, IBeyan_TurDal
    {
        public Beyan_Tur GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_Turleri.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public Beyan_Tur GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_Turleri.Where(gt => gt.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Beyan_Turleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Beyan_Turleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
