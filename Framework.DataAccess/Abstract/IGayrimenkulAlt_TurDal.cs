using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface IGayrimenkulAlt_TurDal : IEntityRepository<GayrimenkulAlt_Tur>
    {
        IEnumerable<GayrimenkulAlt_Tur> GetirListe();
        GayrimenkulAlt_Tur GetById(int id);
        GayrimenkulAlt_Tur GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
