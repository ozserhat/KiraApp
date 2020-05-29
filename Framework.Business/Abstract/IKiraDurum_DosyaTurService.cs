using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IKiraDurum_DosyaTurService
    {
        KiraDurum_DosyaTur Getir(int id);

        KiraDurum_DosyaTur GetirDosyaBeyanTur(int beyanDosyaTurId);

        KiraDurum_DosyaTur GetirKiraDurum(int kiraDurumId);

        IEnumerable<KiraDurum_DosyaTur> GetirDosyaBeyanTurListe(int beyanDosyaTurId);

        IEnumerable<KiraDurum_DosyaTur> GetirKiraDurumListe(int kiraDurumId);

        IEnumerable<KiraDurum_DosyaTur> GetirListe();

        KiraDurum_DosyaTur Ekle(KiraDurum_DosyaTur kiraDurum);

        KiraDurum_DosyaTur Guncelle(KiraDurum_DosyaTur kiraDurum);

        bool Sil(int id);
    }
}
