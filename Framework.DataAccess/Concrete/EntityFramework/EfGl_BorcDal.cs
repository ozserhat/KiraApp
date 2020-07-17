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
    public class EfGl_BorcDal : EfEntityRepositoryBase<GL_BORC, DtContext>, IGl_BorcDal
    {
        public bool Add(IEnumerable<GL_BORC> entities)
        {
            int sonuc = 0;

            using (DtContext context = new DtContext())
            {
                context.GL_BORC.AddRange(entities);

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
                var GL_BORC = context.GL_BORC.FirstOrDefault(i => i.ID == id);

                if (GL_BORC != null)
                {
                    context.GL_BORC.Remove(GL_BORC);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public GL_BORC GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.GL_BORC.Where(gta => gta.ID == id).FirstOrDefault();
            }
        }

        public GL_BORC GetirBeyan(int BeyanId)
        {
            using (DtContext context = new DtContext())
            {
                return context.GL_BORC
                       .Where(gta => gta.BEYAN_ID == BeyanId).FirstOrDefault();
            }
        }

        public IEnumerable<GL_BORC> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                var result = context.GL_BORC.ToList();
                return result;
            }
        }

        public IEnumerable<GL_BORC> GetListByCriterias(TahakkukRequest request)
        {
            List<GL_BORC> result = new List<GL_BORC>();

            using (DtContext context = new DtContext())
            {
                var query = (from th in context.GL_BORC
                             join byn in context.Beyanlar on th.BEYAN_ID equals byn.Id
                             select th
                            ).AsQueryable();

                var beyan = context.Beyanlar.Where(a => a.BeyanNo == request.BeyanNo).FirstOrDefault();

                int sicilNo = (!string.IsNullOrEmpty(request.SicilNo) ? int.Parse(request.SicilNo) : -1);

                query = request.SicilNo != null ? query.Where(x => x.SICIL_NO == sicilNo) : query;

                query = request.BeyanNo != null ? query.Where(x => x.BEYAN_ID == beyan.Id) : query;
                query = request.BeyanYil.HasValue ? query.Where(x => x.BeyanYil == request.BeyanYil) : query;
                query = request.TaksitNo.HasValue ? query.Where(x => x.TAK_NO == request.TaksitNo) : query;
                query = request.TahakkukTarihi.HasValue ? query.Where(x => x.THK_TAR == request.TahakkukTarihi) : query;
                query = request.VadeTarihi.HasValue ? query.Where(x => x.VADE == request.VadeTarihi) : query;
                query = request.Tutar.HasValue ? query.Where(x => x.TUTAR == request.Tutar) : query;
                query = request.Aciklama != null ? query.Where(x => x.ACIKLAMA == request.Aciklama) : query;
                query = request.OdemeDurum_Id.HasValue ? query.Where(x => x.OdemeDurumu == request.OdemeDurumu) : query;

                request.OdemeDurumu = (request.OdemeDurum_Id <= 0 ? false : true);
                
                result = query.ToList();
            }

            return result;

        }
    }
}
