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
    public class EfBeyan_UfeOranDal : EfEntityRepositoryBase<Beyan_UfeOran, DtContext>, IBeyan_UfeOranDal
    {
        public Beyan_UfeOran GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_UfeOranlari.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public Beyan_UfeOran GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_UfeOranlari.Where(gt => gt.Guid == guid).FirstOrDefault();
            }
        }
        public IEnumerable<Beyan_UfeOran> GetirList(int? parametreId)
        {
            using (DtContext context = new DtContext())
            {
                return parametreId == null
                      ? context.Beyan_UfeOranlari.ToList()
                      : context.Beyan_UfeOranlari.Where(a => a.Tur_Id == parametreId).ToList();

            }
        }
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Beyan_UfeOranlari.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Beyan_UfeOranlari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
