using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class ResmiTatillerManager : IResmiTatillerService
    {
        private IResmiTatillerDal _resmiTatilDal;

        public ResmiTatillerManager(IResmiTatillerDal resmiTatilDal)
        {
            _resmiTatilDal = resmiTatilDal;

        }

        public ResmiTatiller Ekle(ResmiTatiller tur)
        {
            return _resmiTatilDal.Add(tur);
        }

        public ResmiTatiller Getir(int id)
        {
            return _resmiTatilDal.GetById(id);
        }

        public IEnumerable<ResmiTatiller> GetirListe()
        {
            return _resmiTatilDal.GetList();
        }

        public ResmiTatiller Guncelle(ResmiTatiller tur)
        {
            return _resmiTatilDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _resmiTatilDal.Delete(id);
        }

        public DateTime TatilGunuKontrol(DateTime tarih)
        {
            bool sonuc = false;

            var resmiTatil = _resmiTatilDal.GetList().Select(a=>a.Tarih);

            DateTime kontrolluTarih = tarih;        

            while (!sonuc)
            {
                if (kontrolluTarih.DayOfWeek == DayOfWeek.Saturday||kontrolluTarih.DayOfWeek == DayOfWeek.Saturday|| resmiTatil.Contains(kontrolluTarih))
                {
                    kontrolluTarih = kontrolluTarih.AddDays(1);
                }
                else
                    sonuc = true;

            }

            return kontrolluTarih;
        }

        void GetAll()
        {
            _resmiTatilDal.GetList();
        }
    }
}
