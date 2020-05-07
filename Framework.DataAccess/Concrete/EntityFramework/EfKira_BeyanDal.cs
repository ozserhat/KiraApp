using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Framework.Entities.Concrete;
using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfKira_BeyanDal : EfEntityRepositoryBase<Kira_Beyan, DtContext>, IKira_BeyanDal
    {
        public Kira_Beyan GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Kira_Beyanlari.Where(gt => gt.Id == id).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Kira_Beyanlari.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Kira_Beyanlari.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Kira_Beyan> GetList()
        {
            using (DtContext context = new DtContext())
            {
                return context.Kira_Beyanlari
                              .Include(b => b.Beyanlar)
                              .Include(bt => bt.Beyanlar.BeyanTur)
                              .Include(k => k.Kiracilar)
                              .Include(g => g.Gayrimenkuller)
                              .Include(m => m.Gayrimenkuller.Mahalleler)
                              .Include(ilc => ilc.Gayrimenkuller.Mahalleler.Ilceler)
                              .Include(ilc => ilc.Gayrimenkuller.Mahalleler.Ilceler.Iller)
                              .ToList();
            }
        }

        public IEnumerable<Kira_Beyan> GetListByCriterias(KiraBeyanRequest request)
        {
            List<Kira_Beyan> result = new List<Kira_Beyan>();

            using (DtContext context = new DtContext())
            {
                var query = context.Kira_Beyanlari
                              .Include(b => b.Beyanlar)
                              .Include(bt => bt.Beyanlar.BeyanTur)
                              .Include(k => k.Kiracilar)
                              .Include(g => g.Gayrimenkuller)
                              .Include(m => m.Gayrimenkuller.Mahalleler)
                              .Include(ilc => ilc.Gayrimenkuller.Mahalleler.Ilceler)
                              .Include(ilc => ilc.Gayrimenkuller.Mahalleler.Ilceler.Iller)
                              .AsQueryable();

                query = request.Guid.HasValue ? query.Where(x => x.Beyanlar.Guid == request.Guid) : query;
                query = request.BeyanTur_Id.HasValue ? query.Where(x => x.Beyanlar.BeyanTur_Id == request.BeyanTur_Id) : query;
                query = request.KiraDurum_Id.HasValue ? query.Where(x => x.Beyanlar.KiraDurum_Id == request.KiraDurum_Id) : query;
                query = request.OdemePeriyotTur_Id.HasValue ? query.Where(x => x.Beyanlar.OdemePeriyotTur_Id == request.OdemePeriyotTur_Id) : query;
                query = request.Gayrimenkul_Id.HasValue ? query.Where(x => x.Gayrimenkul_Id == request.Gayrimenkul_Id) : query;
                query = request.Ilce_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Ilce_Id == request.Ilce_Id) : query;
                query = request.Mahalle_Id.HasValue ? query.Where(x => x.Gayrimenkuller.Mahalle_Id == request.Ilce_Id) : query;


                result = query.ToList();
            }

            return result;
        }

        public Kira_Beyan GetirBeyan(int BeyanId)
        {
            using (DtContext context = new DtContext())
            {
                var query = context.Kira_Beyanlari
                             .Include(b => b.Beyanlar)
                             .Include(b => b.Beyanlar.KiraDurum)
                             .Include(kd=>kd.Beyanlar.OdemePeriyotTur)
                             .Include(bt => bt.Beyanlar.BeyanTur)
                             .Include(k => k.Kiracilar)
                             .Include(g => g.Gayrimenkuller)
                             .Include(m => m.Gayrimenkuller.Mahalleler)
                             .Include(ilc => ilc.Gayrimenkuller.Mahalleler.Ilceler)
                             .Include(ilc => ilc.Gayrimenkuller.Mahalleler.Ilceler.Iller)
                             .Where(a => a.Beyan_Id == BeyanId)
                             .FirstOrDefault();
                return query;
            }
        }
    }
}
