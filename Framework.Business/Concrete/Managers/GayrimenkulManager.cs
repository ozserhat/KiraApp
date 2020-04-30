using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Concrete.Managers
{
    public class GayrimenkulManager : IGayrimenkulService
    {
        private IGayrimenkulDal _gayrimenkulDal;

        public GayrimenkulManager(IGayrimenkulDal gayrimenkulDal)
        {
            _gayrimenkulDal = gayrimenkulDal;
        }

        public Gayrimenkul Ekle(Gayrimenkul tur)
        {
            return _gayrimenkulDal.Add(tur);
        }

        public string GayrimenkulNoUret(int Yil)
        {
            return _gayrimenkulDal.GayrimenkulNoUret(Yil);
        }

        public Gayrimenkul Getir(int id)
        {
            return _gayrimenkulDal.GetById(id);
        }

        public Gayrimenkul GetirGayrimenkul(string GayrimenkulNo)
        {
            return _gayrimenkulDal.GetirGayrimenkul(GayrimenkulNo);
        }

        public Gayrimenkul GetirGuid(Guid guid)
        {
            return _gayrimenkulDal.GetByGuid(guid);
        }

        public IEnumerable<Gayrimenkul> GetirListe()
        {
            return _gayrimenkulDal.GetirListe();
        }

        public Gayrimenkul Guncelle(Gayrimenkul tur)
        {
            return _gayrimenkulDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _gayrimenkulDal.Delete(id);
        }
    }
}
