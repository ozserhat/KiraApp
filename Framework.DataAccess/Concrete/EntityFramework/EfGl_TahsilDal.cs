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
    public class EfGl_TahsilDal : EfEntityRepositoryBase<GL_TAHSIL, DtContext>, IGl_TahsilDal
    {
        public bool Add(IEnumerable<GL_TAHSIL> entities)
        {
            int sonuc = 0;

            using (DtContext context = new DtContext())
            {
                context.GL_TAHSIL.AddRange(entities);

                sonuc = context.SaveChanges();
            }

            if (sonuc > 0)
                return true;

            return false;
        }
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var GL_TAHSIL = context.GL_TAHSIL.FirstOrDefault(i => i.ID == id);

                if (GL_TAHSIL != null)
                {
                    context.GL_TAHSIL.Remove(GL_TAHSIL);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public GL_TAHSIL GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.GL_TAHSIL.Where(gta => gta.ID == id).FirstOrDefault();
            }
        }

        public GL_TAHSIL GetirBeyan(int BeyanId)
        {
            using (DtContext context = new DtContext())
            {
                return context.GL_TAHSIL
                       .Where(gta => gta.BEYAN_ID == BeyanId).FirstOrDefault();
            }
        }

        public IEnumerable<GL_TAHSIL> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                var result = context.GL_TAHSIL.ToList();
                return result;
            }
        }
    }
}
