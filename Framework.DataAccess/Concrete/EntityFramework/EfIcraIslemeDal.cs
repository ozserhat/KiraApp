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
    public class EfIcraIslemeDal : EfEntityRepositoryBase<Beyan_IcraIsleme, DtContext>, IIcraIslemeDal
    {
        public Beyan_IcraIsleme GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Beyan_IcraIsleme.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Beyan_IcraIsleme.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Beyan_IcraIsleme.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
        public IEnumerable<Beyan_IcraIsleme> GetirListe(int beyanId)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Beyan_IcraIsleme.Include(a => a.Beyanlar).Include(d => d.IcraDurumlari).Where(gt => gt.Beyan_Id == beyanId).ToList();

                return result;
            }
        }

        public IEnumerable<Beyan_IcraIsleme> GetirListeAktif(int beyanId)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Beyan_IcraIsleme.Include(a => a.Beyanlar).Include(d => d.IcraDurumlari).Where(gt => gt.AktifMi == true && gt.Beyan_Id == beyanId).ToList();

                return result;
            }
        }
    }
}
