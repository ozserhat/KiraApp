using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class Kira_DurumManager : IKira_DurumService
    {
        private IKira_DurumDal _kiraDurumDal;

        public Kira_DurumManager(IKira_DurumDal kiraDurumDal)
        {
            _kiraDurumDal = kiraDurumDal;
        }

        public Kira_Durum Ekle(Kira_Durum kiraDurum)
        {
            return _kiraDurumDal.Add(kiraDurum);
        }

        public Kira_Durum Getir(int id)
        {
            return _kiraDurumDal.GetById(id);
        }

        public Kira_Durum GetirGuid(Guid guid)
        {
            return _kiraDurumDal.GetByGuid(guid);
        }

        public IEnumerable<Kira_Durum> GetirListe()
        {
            return _kiraDurumDal.GetList();
        }
        public IEnumerable<Kira_Durum> GetirListeAktif()
        {
            return _kiraDurumDal.GetList().Where(a => a.AktifMi == true);
        }

        public Kira_Durum Guncelle(Kira_Durum kiraDurum)
        {
            return _kiraDurumDal.Update(kiraDurum);
        }

        public bool Sil(int id)
        {
            return _kiraDurumDal.Delete(id);
        }
    }
}
