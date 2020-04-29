using System;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;


namespace Framework.DataAccess.Abstract
{
    public interface IBeyanDosya_TurDal : IEntityRepository<BeyanDosya_Tur>
    {
        BeyanDosya_Tur GetById(int id);
        BeyanDosya_Tur GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
