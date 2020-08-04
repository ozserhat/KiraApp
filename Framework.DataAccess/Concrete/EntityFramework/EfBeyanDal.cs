using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfBeyanDal : EfEntityRepositoryBase<Beyan, DtContext>, IBeyanDal
    {
        public Beyan GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Beyanlar.Where(gta => gta.Id == id).FirstOrDefault();
                if (result != null)
                    return result;
                else
                    return context.Beyanlar.Where(b => b.EskiBeyanId == id).FirstOrDefault();
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
                var beyan = context.Beyanlar.FirstOrDefault(i => i.Id == id);

                if (beyan != null)
                {
                    context.Beyanlar.Remove(beyan);
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
                return context.Beyanlar
                              .ToList();
            }
        }

        public string BeyanNoUret(int Yil)
        {
            try
            {
                using (DtContext context = new DtContext())
                {
                    var result = context.Beyanlar.Where(x => x.OlusturulmaTarihi.Value.Year == Yil).OrderByDescending(x => x.Id).First().BeyanNo.Split('-').Last();

                    int Numara = int.Parse(result);

                    Numara++;

                    string Num = Numara.ToString().PadLeft(7, '0');

                    return Num;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return "000001";
            }
        }
    }
}

