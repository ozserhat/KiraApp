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
    public class TahakkukManager : ITahakkukService
    {
        private ITahakkukDal _tahakkukDal;

        public TahakkukManager(ITahakkukDal tahakkukDal)
        {
            _tahakkukDal= tahakkukDal;
        }

        public Tahakkuk Ekle(Tahakkuk tahakkuk)
        {
            return _tahakkukDal.Add(tahakkuk);
        }

        public bool Ekle(IEnumerable<Tahakkuk> entities)
        {
            return _tahakkukDal.Add(entities);
        }

        public Tahakkuk GetByGuid(Guid guid)
        {
            return _tahakkukDal.GetByGuid(guid);
        }

        public Tahakkuk GetById(int id)
        {
            return _tahakkukDal.GetById(id);
        }

        public Tahakkuk GetirBeyan(int BeyanId)
        {
            return _tahakkukDal.GetirBeyan(BeyanId);
        }

        public Tahakkuk GetirGayrimenkul(int GayrimenkulId)
        {
            return _tahakkukDal.GetirGayrimenkul(GayrimenkulId);
        }

        public Tahakkuk GetirKiraci(int KiraciId)
        {
            return _tahakkukDal.GetirKiraci(KiraciId);
        }

        public IEnumerable<Tahakkuk> GetirListe()
        {
            return _tahakkukDal.GetirListe();
        }

        public List<Tahakkuk> GetirListe(int KiraBeyanId)
        {
            return _tahakkukDal.GetirListeBeyanId(KiraBeyanId);
        }

        public Tahakkuk Guncelle(Tahakkuk tahakkuk)
        {
            return _tahakkukDal.Update(tahakkuk);
        }

        public bool Sil(int id)
        {
            return _tahakkukDal.Delete(id);
        }
    }
}
