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
    public class EfKiraParametreDal : EfEntityRepositoryBase<KiraParametre, DtContext>, IKiraParametreDal
    {
        public KiraParametre GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraParametreleri.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public KiraParametre GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.KiraParametreleri.Where(gt => gt.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var parameter = context.KiraParametreleri.FirstOrDefault(i => i.Id == id);

                if (parameter != null)
                {
                    context.KiraParametreleri.Remove(parameter);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
