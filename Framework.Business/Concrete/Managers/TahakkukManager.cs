using Framework.Business.Abstract;
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
    public class TahakkukManager : ITahakkukService
    {
        private readonly ITahakkukDal _tahakkukDal;

        public TahakkukManager(ITahakkukDal tahakkukDal)
        {
            _tahakkukDal = tahakkukDal;
        }

        public Tahakkuk Ekle(Tahakkuk tahakkuk)
        {
            return _tahakkukDal.Add(tahakkuk);
        }

        public bool Ekle(List<Tahakkuk> entities)
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
      
        public Tahakkuk Getir(int id)
        {
            return _tahakkukDal.GetById(id);
        }
     
        public IEnumerable<Tahakkuk> GetirSorguListe(TahakkukRequest request)
        {
            return _tahakkukDal.GetListByCriterias(request);
        }
      
        public IEnumerable<Tahakkuk> GetirListeAktif()
        {
            return _tahakkukDal.GetirListe().Where(a => a.AktifMi == (int)EnmIslemDurumu.Aktif);
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

        public List<Tahakkuk> GetirOdemesiGecikenTahakkuklar()
        {
            var result = _tahakkukDal.GetirListe().Where(a => a.AktifMi == (int)EnmIslemDurumu.Aktif && DateTime.Now > a.VadeTarihi.Value).ToList();
            return result;
        }

        public List<Tahakkuk> GetirArtisiGelenTahakkuklar()
        {
            var result = _tahakkukDal.GetirListe().Where(a => a.AktifMi == (int)EnmIslemDurumu.Aktif && a.Kira_Beyani.Beyanlar.KiraBaslangicTarihi.Value.AddYears(1).ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            return result;
        }

        public List<Tahakkuk> GetirOdemesiGelenTahakkuklar()
        {
            var result = _tahakkukDal.GetirListe().Where(a => a.AktifMi == (int)EnmIslemDurumu.Aktif && a.VadeTarihi.Value.ToShortDateString() == DateTime.Now.AddDays(-7).ToShortDateString()).ToList();
            return result;
        }

        public List<Tahakkuk> GetirSozlemesiBitenBeyanlar()
        {
            var result = _tahakkukDal.GetirListe().Where(a => a.AktifMi == (int)EnmIslemDurumu.Aktif && a.Kira_Beyani.Beyanlar.SozlesmeBitisTarihi.Value.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            return result;
        }
    }
}
