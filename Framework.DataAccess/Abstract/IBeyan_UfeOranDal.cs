using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IBeyan_UfeOranDal : IEntityRepository<Beyan_UfeOran>
    {
        Beyan_UfeOran GetById(int id);
        Beyan_UfeOran GetByGuid(Guid guid);
        IEnumerable<Beyan_UfeOran> GetirList(int? parametreId);
        bool Delete(int id);
    }
}
