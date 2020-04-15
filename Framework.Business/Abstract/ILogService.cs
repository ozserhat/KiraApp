using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface ILogService
    {
        IEnumerable<Log> GetAll();
        T JsonDeserialize<T>(string jsonString);

        Log GetById(int id);

        Log Ekle(Log log);

        Log Guncelle(Log log);

        bool Sil(int id);
    }
}
