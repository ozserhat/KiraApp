using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfBeyanDal : EfEntityRepositoryBase<Beyan, DtContext>, IBeyanDal
    {
        public Beyan GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyanlar.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Beyan GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyanlar.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Beyanlar.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Beyanlar.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Beyan> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyanlar.ToList();
            }
        }
    }
}

