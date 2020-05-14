using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IGayrimenkulTurService
    {
        IEnumerable<GayrimenkulTur> GetirListe();
        IEnumerable<GayrimenkulTur> GetirListeAktif();

        GayrimenkulTur Getir(int id);

        GayrimenkulTur GetirGuid(Guid guid);

        GayrimenkulTur Ekle(GayrimenkulTur tur);

        GayrimenkulTur Guncelle(GayrimenkulTur tur);

        bool Sil(int id);
    }
}
