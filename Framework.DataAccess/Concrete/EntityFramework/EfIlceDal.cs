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
    public class EfIlceDal : EfEntityRepositoryBase<Ilce, DtContext>, IIlceDal
    {
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var ilce = context.Ilceler.FirstOrDefault(i => i.Id == id);

                if (ilce != null)
                {
                    context.Ilceler.Remove(ilce);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public Ilce GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Ilceler.Include(x => x.Iller).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Ilce GetByName(string name)
        {
            using (DtContext context = new DtContext())
            {
                return context.Ilceler.Include(x => x.Iller).Where(gta => gta.Ad.Contains(name)).FirstOrDefault();
            }
        }

        public IEnumerable<Ilce> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Ilceler.Include(x => x.Iller).ToList();
            }
        }
    }
}
