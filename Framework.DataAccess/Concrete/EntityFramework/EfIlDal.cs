using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Framework.Entities.Concrete;
using Framework.DataAccess.Abstract;
using Framework.Core.DataAccess.EntityFramework;
using System.Linq.Expressions;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfIlDal : EfEntityRepositoryBase<Il, DtContext>, IIlDal
    {
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var il = context.Iller.FirstOrDefault(i => i.Id == id);

                if (il != null)
                {
                    context.Iller.Remove(il);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

    

        public Il GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Iller.FirstOrDefault(i => i.Id == id);
            }
        }



        public IEnumerable<Il> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Iller.ToList();
            }
        }
    }
}
