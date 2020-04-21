using Framework.Business.Abstract;
using Framework.Business.ValidationRules.FluentValidation;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Framework.Business.Concrete.Managers
{
    public class LogsManager : ILogService
    {
        private ILogsDal _logDal;

        public LogsManager(ILogsDal logDal)
        {
            _logDal = logDal;
        }

        public T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            T obj = JsonConvert.DeserializeObject<T>(jsonString);
            //using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            //{
            //    obj = (T)ser.ReadObject(ms);
            //}

            return obj;
        }

        public Log Ekle(Log log)
        {
            return _logDal.Ekle(log);
        }

        public System.Collections.Generic.IEnumerable<Log> GetAll()
        {
            return _logDal.GetAll();
        }

        public Log GetById(int id)
        {
            return _logDal.GetById(id);
        }

        public Log Guncelle(Log log)
        {
            return _logDal.Guncelle(log);
        }

        public bool Sil(int id)
        {
            return _logDal.Sil(id);
        }
    }
}
