using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface IIlDal : IEntityRepository<Il>
    {
        Il GetById(int id);
        IEnumerable<Il> GetirListe();
        bool Delete(int id);
    }
}
