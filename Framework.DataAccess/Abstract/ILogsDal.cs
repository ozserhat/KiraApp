using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Framework.DataAccess.Abstract
{
    public interface ILogsDal
    {
        IEnumerable<Log> GetAll();

        Log GetById(int id);

        Log Ekle(Log role);

        Log Guncelle(Log role);

        bool Sil(int id);
    }
}
