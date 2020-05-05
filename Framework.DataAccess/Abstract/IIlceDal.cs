using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface IIlceDal : IEntityRepository<Ilce>
    {
        Ilce GetById(int id);
        Ilce GetByName(string name);
        IEnumerable<Ilce> GetirListe();
        bool Delete(int id);
    }
}
