using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class KiraDurum_DosyaTurManager : IKiraDurum_DosyaTurService
    {
        private IKiraDurum_DosyaTurDal _kiraDurumDal;

        public KiraDurum_DosyaTurManager(IKiraDurum_DosyaTurDal kiraDurumDal)
        {
            _kiraDurumDal = kiraDurumDal;
        }

        public KiraDurum_DosyaTur Ekle(KiraDurum_DosyaTur kiraDurum)
        {
            return _kiraDurumDal.Add(kiraDurum);
        }
        public KiraDurum_DosyaTur Guncelle(KiraDurum_DosyaTur kiraDurum)
        {
            return _kiraDurumDal.Update(kiraDurum);
        }

        public bool Sil(int id)
        {
            return _kiraDurumDal.Delete(id);
        }
        public KiraDurum_DosyaTur Getir(int id)
        {
            return _kiraDurumDal.GetById(id);
        }

        public KiraDurum_DosyaTur GetirDosyaBeyanTur(int beyanDosyaTurId)
        {
            return _kiraDurumDal.GetirDosyaBeyanTur(beyanDosyaTurId);
        }

        public KiraDurum_DosyaTur GetirKiraDurum(int kiraDurumId)
        {
            return _kiraDurumDal.GetirKiraDurum(kiraDurumId);
        }

        public IEnumerable<KiraDurum_DosyaTur> GetirDosyaBeyanTurListe(int beyanDosyaTurId)
        {
            return _kiraDurumDal.GetirDosyaBeyanTurListe(beyanDosyaTurId);
        }

        public IEnumerable<KiraDurum_DosyaTur> GetirKiraDurumListe(int kiraDurumId)
        {
            return _kiraDurumDal.GetirKiraDurumListe(kiraDurumId);
        }

        public IEnumerable<KiraDurum_DosyaTur> GetirListe()
        {
            return _kiraDurumDal.GetirListe();
        }
    }
}
