using Framework.Business.Abstract;
using Framework.Core.Aspects.Postsharp.TransactionAspects;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using Framework.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class BeyanManager : IBeyanService
    {
        private IBeyanDal _beyanDal;
        private IBeyan_DosyaDal _beyanDosyaDal;


        public BeyanManager(IBeyanDal beyanDal, IBeyan_DosyaDal beyanDosyaDal)
        {
            _beyanDal = beyanDal;
            _beyanDosyaDal = beyanDosyaDal;
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
    }
}
