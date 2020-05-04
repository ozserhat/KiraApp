using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface IBeyanDal : IEntityRepository<Beyan>
    {
        IEnumerable<Beyan> GetirListe();
        Beyan GetById(int id);
        Beyan GetByGuid(Guid guid);
        bool Delete(int id);
        string BeyanNoUret(int Yil);

    }
}
