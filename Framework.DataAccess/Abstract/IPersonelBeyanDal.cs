using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Framework.DataAccess.Abstract
{
    public interface IPersonelBeyanDal : IEntityRepository<PersonelBeyan>
    {
        IEnumerable<PersonelBeyan> GetirListe();
        PersonelBeyan GetById(int id);
        bool Delete(int id);
        bool BeyanSil(int beyanId);

    }
}
