using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using Framework.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Framework.Business.Concrete.Managers
{
    public class BeyanManager : IBeyanService
    {
        private IBeyanDal _beyanDal;
        private IBeyan_DosyaDal _beyanDosyaDal;
        private ITahakkukService _tahakkukService;
        private IKira_BeyanService _kira_BeyanService;
        private IKiraciService _kiraciService;


        public BeyanManager(IBeyanDal beyanDal, IBeyan_DosyaDal beyanDosyaDal, ITahakkukService tahakkukService, IKira_BeyanService kira_BeyanService, IKiraciService kiraciService)
        {
            _beyanDal = beyanDal;
            _beyanDosyaDal = beyanDosyaDal;
            _tahakkukService = tahakkukService;
            _kira_BeyanService = kira_BeyanService;
            _kiraciService = kiraciService;
        }

        public string BeyanNoUret(int Yil)
        {
            return _beyanDal.BeyanNoUret(Yil);
        }

        public Beyan Ekle(Beyan tur)
        {
            return _beyanDal.Add(tur);
        }


        //[TransactionScopeAspect]
        public bool TransactionTest(Beyan beyan, List<Beyan_Dosya> dosya)
        {
            var result = true;

            try
            {
                var beyanResult = Ekle(beyan);
                Beyan_DosyaManager dosyaManager = new Beyan_DosyaManager(_beyanDosyaDal);
                var dosyaResult = dosyaManager.Ekle(dosya.FirstOrDefault());
            }
            catch (Exception)
            {

                result = false;
            }

            return result;
        }

        public Beyan Getir(int id)
        {
            return _beyanDal.GetById(id);
        }

        public Beyan GetirGuid(Guid guid)
        {
            return _beyanDal.GetByGuid(guid);
        }

        public IEnumerable<Beyan> GetirListe()
        {
            return _beyanDal.GetirListe();
        }

        public IEnumerable<Beyan> GetirListeAktif()
        {
            return _beyanDal.GetirListe().Where(a => a.AktifMi == (int)EnmIslemDurumu.Aktif).ToList();
        }

        public Beyan Guncelle(Beyan tur)
        {
            return _beyanDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _beyanDal.Delete(id);
        }
        public bool KiraBeyanIslemleri(KiraBeyanIslemleri islemler)
        {
            var result = false;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (islemler.PasifeAlinanlar != null)
                        PasifeAlinanBeyanBilgileri(islemler.PasifeAlinanlar);
                    if (islemler.Kapananlar != null)
                        KapananBeyanBilgileri(islemler.PasifeAlinanlar);
                    if (islemler.Eklenenler != null)
                        EklenecekBeyanBilgileri(islemler.Eklenenler);
                    scope.Complete();
                    return result;
                }
                catch (Exception)
                {
                    scope.Dispose();
                }

            }
            return result;
        }

        private void EklenecekBeyanBilgileri(KiraBeyanModel eklenenler)
        {
            var sicilId = eklenenler.KiraBeyan.Kiraci_Id;
            var beyanId = 0;
            var kiraBeyanId = 0;
            if (eklenenler.Kiraci != null)
            {
                var sicilEkle = _kiraciService.Ekle(eklenenler.Kiraci);
                sicilId = sicilEkle.Id;
            }
            if (eklenenler.Beyan != null)
            {
                var beyanEkle = Ekle(eklenenler.Beyan);
                beyanId = beyanEkle.Id;
            }
            if (eklenenler.KiraBeyan != null)
            {
                var kiraBeyan = KiraBeyanEkle(eklenenler.KiraBeyan);
                kiraBeyanId = kiraBeyan.Id;
            }
            if (eklenenler.BeyanDosyalar != null)
            {
                beyanId = beyanId == 0 ? eklenenler.BeyanDosyalar.FirstOrDefault().Id : beyanId;
                BeyanDosyaListesiKaydet(eklenenler.BeyanDosyalar, beyanId);

            }
            if (eklenenler.Tahakkuklar != null)
            {
                kiraBeyanId = kiraBeyanId == 0 ? eklenenler.Tahakkuklar.FirstOrDefault().Id : kiraBeyanId;
                TahakukListesiKaydet(eklenenler.Tahakkuklar, kiraBeyanId);
            }
        }

        private Kira_Beyan KiraBeyanEkle(Kira_Beyan kiraBeyan)
        {
            return _kira_BeyanService.Ekle(kiraBeyan);

        }

        private void TahakukListesiKaydet(List<Tahakkuk> tahakukkList, int kiraBeyanId)
        {
            foreach (var item in tahakukkList)
            {
                item.KiraBeyan_Id = kiraBeyanId;
                _tahakkukService.Ekle(item);
            }
        }
        private void BeyanDosyaListesiKaydet(List<Beyan_Dosya> doyaListesi, int beyanId)
        {
            foreach (var item in doyaListesi)
            {
                item.Beyan_Id = beyanId;
                var result = _beyanDosyaDal.Add(item);
                if (result.Id > 0)
                {
                    byte[] fileBytes = Convert.FromBase64String(item.BeyanDosya);
                    System.IO.File.WriteAllBytes(item.FilePath + item.Ad, fileBytes);
                }
            }
        }

        private bool PasifeAlinanBeyanBilgileri(KiraBeyanModel pasifealinan)
        {
            if (pasifealinan.Tahakkuklar.Where(x => x.OdemeDurumu == true).ToList().Count > 0)
            {
                throw new Exception("Ödenmiş tahakukk bilgisi bulunmaktadır. Güncelleme işlemi yapılamaz.");
            }
            pasifealinan.Beyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            Guncelle(pasifealinan.Beyan);

            foreach (var item in pasifealinan.Tahakkuklar)
            {
                item.AktifMi = (int)EnmIslemDurumu.Pasif;
                _tahakkukService.Guncelle(item);
            }

            //Kira Beyan Sayfası Pasife Alınır.

            pasifealinan.KiraBeyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            _kira_BeyanService.Guncelle(pasifealinan.KiraBeyan);
            return true;
        }

        private bool KapananBeyanBilgileri(KiraBeyanModel pasifealinan)
        {
            if (pasifealinan.Tahakkuklar.Where(x => x.OdemeDurumu == true).ToList().Count > 0)
            {
                throw new Exception("Ödenmiş tahakukk bilgisi bulunmaktadır. Güncelleme işlemi yapılamaz.");
            }
            pasifealinan.Beyan.AktifMi = (int)EnmIslemDurumu.Kapandı;
            Guncelle(pasifealinan.Beyan);

            foreach (var item in pasifealinan.Tahakkuklar)
            {
                item.AktifMi = (int)EnmIslemDurumu.Kapandı;
                _tahakkukService.Guncelle(item);
            }
            //Kira Beyan Sayfası Pasife Alınır.
            pasifealinan.KiraBeyan.AktifMi = (int)EnmIslemDurumu.Kapandı;
            _kira_BeyanService.Guncelle(pasifealinan.KiraBeyan);
            return true;
        }
    }
}
