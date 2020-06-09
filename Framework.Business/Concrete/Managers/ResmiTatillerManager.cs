using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<ResmiTatiller> GetirListeAktif()
        {
            return _resmiTatilDal.GetList().Where(a => a.AktifMi == true);
        }
        public ResmiTatiller Guncelle(ResmiTatiller tur)
        {
            return _resmiTatilDal.Update(tur);
        }


        public bool Sil(int id)
        {
            return _resmiTatilDal.Delete(id);
        }

        public int TarihAraligiGunSayisi(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            TimeSpan timeSpan = bitisTarihi.Subtract(baslangicTarihi);
            return timeSpan.Days;
        }

        public DateTime TatilGunuKontrol(DateTime tarih)
        {
            bool sonuc = false;

            var resmiTatil = _resmiTatilDal.GetList().Select(a => a.Tarih);

            DateTime kontrolluTarih = tarih;

            while (!sonuc)
            {
                if (kontrolluTarih.DayOfWeek == DayOfWeek.Saturday || kontrolluTarih.DayOfWeek == DayOfWeek.Saturday || resmiTatil.Contains(kontrolluTarih))
                {
                    kontrolluTarih = kontrolluTarih.AddDays(1);
                }
                else
                    sonuc = true;

            }

            return kontrolluTarih;
        }

        public int TatilGunuKontrol(DateTime baslangic, DateTime bitisTarihi, string[] tarihler, bool? resmiTatilVarmi)
        {
            int hesapSayisi = 0;

            bool sonuc = false;

            var resmiTatil = _resmiTatilDal.GetList().Select(a => a.Tarih);

            for (DateTime x = baslangic; x <= bitisTarihi; x = x.AddDays(1))
            {
                if (tarihler != null)
                    sonuc = tarihler.Contains(x.DayOfWeek.ToString());

                if (sonuc)
                    hesapSayisi++;

                if (!sonuc && resmiTatilVarmi.HasValue && resmiTatilVarmi.Value && resmiTatil.Contains(x))
                    hesapSayisi++;
            }

            return hesapSayisi;
        }

        void GetAll()
        {
            _resmiTatilDal.GetList();
        }
    }
}
