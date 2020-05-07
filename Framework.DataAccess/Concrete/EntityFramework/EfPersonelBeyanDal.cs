using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfPersonelBeyanDal : EfEntityRepositoryBase<PersonelBeyan, DtContext>, IPersonelBeyanDal
    {
        public PersonelBeyan GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.PersonelBeyanlari.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.PersonelBeyanlari.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.PersonelBeyanlari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public bool BeyanSil(int beyanId)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.PersonelBeyanlari.FirstOrDefault(i => i.Id == beyanId);

                if (tur != null)
                {
                    context.PersonelBeyanlari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
        

        public IEnumerable<PersonelBeyan> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.PersonelBeyanlari
                              .ToList();
            }
        }

    }
}
