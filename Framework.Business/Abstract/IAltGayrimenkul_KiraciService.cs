using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.Business.Abstract
{
    public interface IAltGayrimenkul_KiraciService
    {
        IEnumerable<AltGayrimenkul_Kiraci> GetirListe(int gayrimenkulId);

        IEnumerable<AltGayrimenkul_Kiraci> GetirListeAktif(int gayrimenkulId);

        AltGayrimenkul_Kiraci Getir(int id);

        AltGayrimenkul_Kiraci GetirGuid(Guid guid);

        bool Ekle(AltGayrimenkul_Kiraci tur);

        AltGayrimenkul_Kiraci Guncelle(AltGayrimenkul_Kiraci tur);
        bool Guncelle(IEnumerable<AltGayrimenkul_Kiraci> entities);

        bool Sil(int id);
    }
}
