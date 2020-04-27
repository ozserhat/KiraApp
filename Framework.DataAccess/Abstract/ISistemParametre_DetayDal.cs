using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface ISistemParametre_DetayDal : IEntityRepository<SistemParametre_Detay>
    {
        SistemParametre_Detay GetById(int id);
        SistemParametre_Detay GetByGuid(Guid guid);
        IEnumerable<SistemParametre_Detay> GetirListe(int? parametreId);

        bool Delete(int id);
    }
}
