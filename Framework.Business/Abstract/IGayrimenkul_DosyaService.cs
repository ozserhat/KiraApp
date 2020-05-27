using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
   public interface IGayrimenkul_DosyaService
    {
        IEnumerable<Gayrimenkul_Dosya> GetirListe();

        IEnumerable<Gayrimenkul_Dosya> GetirListeAktif();
        Gayrimenkul_Dosya Getir(int id);

        Gayrimenkul_Dosya GetirGuid(Guid guid);

        IEnumerable<Gayrimenkul_Dosya> GetirGayrimenkulId(int GayrimenkulId);

        Gayrimenkul_Dosya Ekle(Gayrimenkul_Dosya tur);

        Gayrimenkul_Dosya Guncelle(Gayrimenkul_Dosya tur);

        bool Sil(int id);
    }
}
