using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class KiraciManager : IKiraciService
    {
        private IKiraciDal _kiraciDal;

        public KiraciManager(IKiraciDal kiraciDal)
        {
            _kiraciDal = kiraciDal;
        }

        public Kiraci Ekle(Kiraci kiraci)
        {
            return _kiraciDal.Add(kiraci);
        }

        public Kiraci Getir(int id)
        {
            return _kiraciDal.GetById(id);
        }

        public Kiraci GetirGuid(Guid guid)
        {
            return _kiraciDal.GetByGuid(guid);
        }

        public IEnumerable<Kiraci> GetirListe()
        {
            return _kiraciDal.GetList();
        }
        public IEnumerable<Kiraci> GetirListeAktif()
        {
            return _kiraciDal.GetList().Where(a => a.AktifMi == true);
        }

        public Kiraci GetirSicilNo(int SicilNo)
        {
            return _kiraciDal.Get(a => a.SicilNo == SicilNo);
        }

        public Kiraci GetirTcNo(int TcKimlikNo)
        {
            return _kiraciDal.Get(a => a.TcKimlikNo == TcKimlikNo);
        }

        public IEnumerable<Kiraci> GetirTurId(int TurId)
        {
            return _kiraciDal.GetList(a => a.KiraciTur_Id == TurId);
        }

        public Kiraci GetirVergiNo(int VergiNo)
        {
            return _kiraciDal.Get(a => a.VergiNo == VergiNo);
        }

        public Kiraci Guncelle(Kiraci kiraci)
        {
            return _kiraciDal.Update(kiraci);
        }

        public bool Sil(int id)
        {
            return _kiraciDal.Delete(id);
        }
    }
}
