using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface ITahakkukDal : IEntityRepository<Tahakkuk>
    {
        IEnumerable<Tahakkuk> GetirListe();
        List<Tahakkuk> GetirListeBeyanId(int KiraBeyanId);
        IEnumerable<Tahakkuk> GetListByCriterias(TahakkukRequest request);
        Tahakkuk GetById(int id);
        Tahakkuk GetByGuid(Guid guid);
        Tahakkuk GetirBeyan(int BeyanId);
        Tahakkuk GetirKiraci(int KiraciId);
        Tahakkuk GetirGayrimenkul(int GayrimenkulId);
        bool Add(IEnumerable<Tahakkuk> entities);
        bool Delete(int id);
    }
}
