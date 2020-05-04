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
    public class BeyanManager : IBeyanService
    {
        private IBeyanDal _beyanDal;

        public BeyanManager(IBeyanDal beyanDal)
        {
            _beyanDal = beyanDal;
        }

        public string BeyanNoUret(int Yil)
        {
            return _beyanDal.BeyanNoUret(Yil);
        }

        public Beyan Ekle(Beyan tur)
        {
            return _beyanDal.Add(tur);
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
