using System;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface IGayrimenkulDosya_TurDal : IEntityRepository<GayrimenkulDosya_Tur>
    {
        GayrimenkulDosya_Tur GetById(int id);
        GayrimenkulDosya_Tur GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
