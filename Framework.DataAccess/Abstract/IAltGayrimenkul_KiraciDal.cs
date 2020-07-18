using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IAltGayrimenkul_KiraciDal : IEntityRepository<AltGayrimenkul_Kiraci>
    {
        AltGayrimenkul_Kiraci GetById(int id);
        AltGayrimenkul_Kiraci GetByGuid(Guid guid);
        bool Ekle(AltGayrimenkul_Kiraci entities);
        bool Guncelle(IEnumerable<AltGayrimenkul_Kiraci> entities);

        IEnumerable<AltGayrimenkul_Kiraci> GetirListe(int gayrimenkulId);
        IEnumerable<AltGayrimenkul_Kiraci> GetirListeAktif(int gayrimenkulId);

        bool Delete(int id);
    }
}
