using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;


namespace Framework.DataAccess.Abstract
{
    public interface IKiraDurum_DosyaTurDal : IEntityRepository<KiraDurum_DosyaTur>
    {
        KiraDurum_DosyaTur GetById(int id);
       
        KiraDurum_DosyaTur GetirDosyaBeyanTur(int beyanDosyaTurId);

        KiraDurum_DosyaTur GetirKiraDurum(int kiraDurumId);

        IEnumerable<KiraDurum_DosyaTur> GetirDosyaBeyanTurListe(int beyanDosyaTurId);

        IEnumerable<KiraDurum_DosyaTur> GetirKiraDurumListe(int kiraDurumId);

        IEnumerable<KiraDurum_DosyaTur> GetirListe();

        bool Delete(int id);
    }
}
