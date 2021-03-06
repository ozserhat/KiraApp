﻿using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IGayrimenkulDal : IEntityRepository<Gayrimenkul>
    {
        IEnumerable<Gayrimenkul> GetirListe();
        IEnumerable<Gayrimenkul> GetListByCriteriasGayrimenkul(GayrimenkulBeyanRequest request);
        Gayrimenkul GetById(int id);
        Gayrimenkul GetByGuid(Guid guid);
        bool Delete(int id);
        Gayrimenkul GetirGayrimenkul(string GayrimenkulNo);
        string GayrimenkulNoUret(int Yil);
    }
}
