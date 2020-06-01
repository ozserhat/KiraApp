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
    public class BeyanDosya_TurManager : IBeyanDosya_TurService
    {
        private IBeyanDosya_TurDal _beyanDosyaTurDal;

        public BeyanDosya_TurManager(IBeyanDosya_TurDal beyanDosyaTurDal)
        {
            _beyanDosyaTurDal = beyanDosyaTurDal;
        }

        public BeyanDosya_Tur Ekle(BeyanDosya_Tur tur)
        {
            return _beyanDosyaTurDal.Add(tur);
        }

        public BeyanDosya_Tur Getir(int id)
        {
            return _beyanDosyaTurDal.GetById(id);
        }

        public BeyanDosya_Tur GetirGuid(Guid guid)
        {
            return _beyanDosyaTurDal.GetByGuid(guid);
        }

        public IEnumerable<BeyanDosya_Tur> GetirKapamaListe()
        {
            return _beyanDosyaTurDal.GetList().Where(a => a.KapatmaMi.Value);
        }

        public IEnumerable<BeyanDosya_Tur> GetirListe()
        {
            return _beyanDosyaTurDal.GetList().Where(a => !a.KapatmaMi.Value);
        }

        public IEnumerable<BeyanDosya_Tur> GetirListeAktif()
        {
            return _beyanDosyaTurDal.GetList().Where(a => a.AktifMi == true && !a.KapatmaMi.Value);
        }
        public BeyanDosya_Tur Guncelle(BeyanDosya_Tur tur)
        {
            return _beyanDosyaTurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _beyanDosyaTurDal.Delete(id);
        }

        void GetAll()
        {
            _beyanDosyaTurDal.GetList();
        }
    }
}
