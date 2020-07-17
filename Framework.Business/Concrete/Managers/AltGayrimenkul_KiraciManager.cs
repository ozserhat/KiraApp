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
    public class AltGayrimenkul_KiraciManager : IAltGayrimenkul_KiraciService
    {
        private IAltGayrimenkul_KiraciDal _altKiraciService;

        public AltGayrimenkul_KiraciManager(IAltGayrimenkul_KiraciDal altKiraciService)
        {
            _altKiraciService = altKiraciService;
        }

        public AltGayrimenkul_Kiraci Ekle(AltGayrimenkul_Kiraci tur)
        {
            return _altKiraciService.Add(tur);
        }

        public AltGayrimenkul_Kiraci Getir(int id)
        {
            return _altKiraciService.GetById(id);
        }

        public AltGayrimenkul_Kiraci GetirGuid(Guid guid)
        {
            return _altKiraciService.GetByGuid(guid);
        }

        public IEnumerable<AltGayrimenkul_Kiraci> GetirListe(int gayrimenkulId)
        {
            return _altKiraciService.GetirListe(gayrimenkulId);
        }

        public IEnumerable<AltGayrimenkul_Kiraci> GetirListeAktif(int gayrimenkulId)
        {
            return _altKiraciService.GetirListeAktif(gayrimenkulId);
        }

        public AltGayrimenkul_Kiraci Guncelle(AltGayrimenkul_Kiraci tur)
        {
            return _altKiraciService.Update(tur);
        }

        public bool Sil(int id)
        {
            return _altKiraciService.Delete(id);
        }

        void GetAll()
        {
            _altKiraciService.GetList();
        }
    }
}
