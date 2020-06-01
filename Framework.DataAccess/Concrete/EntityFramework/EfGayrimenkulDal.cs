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
    public class EfGayrimenkulDal : EfEntityRepositoryBase<Gayrimenkul, DtContext>, IGayrimenkulDal
    {
        public Gayrimenkul GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkuller.Include(gt => gt.GayrimenkulTur).Include(a => a.Mahalleler)
                                             .Include(a => a.Mahalleler.Ilceler)
                                             .Include(a => a.Mahalleler.Ilceler.Iller).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public Gayrimenkul GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkuller.Include(gt => gt.GayrimenkulTur).Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.Gayrimenkuller.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.Gayrimenkuller.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

        public IEnumerable<Gayrimenkul> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkuller.Include(gt => gt.GayrimenkulTur)
                                             .Include(a => a.Mahalleler)
                                             .Include(a => a.Mahalleler.Ilceler)
                                             .Include(a => a.Mahalleler.Ilceler.Iller)
                                             .Include(a => a.Kira_Durumlari)
                                             .ToList();
            }
        }

        public string GayrimenkulNoUret(int Yil)
        {
            try
            {
                using (DtContext context = new DtContext())
                {
                    var resulNum = "";
                    var result = context.Gayrimenkuller.Where(x => x.OlusturulmaTarihi.Value != null && x.OlusturulmaTarihi.Value.Year == Yil).OrderByDescending(x => x.Id).FirstOrDefault();
                    if (result != null)
                        resulNum = result.GayrimenkulNo.Split('-').Last();
                    else
                        resulNum = "1";
                    int Numara = int.Parse(resulNum);

                    Numara++;

                    string Num = Numara.ToString().PadLeft(7, '0');

                    return Num;
                }
            }
            catch (Exception ex)
            {
                return "000001";
            }
        }

        public Gayrimenkul GetirGayrimenkul(string GayrimenkulNo)
        {
            using (DtContext context = new DtContext())
            {
                return context.Gayrimenkuller.Include(gt => gt.GayrimenkulTur)
                                             .Include(a => a.Mahalleler)
                                             .Include(a => a.Mahalleler.Ilceler)
                                             .Include(a => a.Mahalleler.Ilceler.Iller)
                                             .Where(g => g.GayrimenkulNo == GayrimenkulNo)
                                             .FirstOrDefault();
            }
        }
    }
}
