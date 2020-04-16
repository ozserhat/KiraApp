using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfLogDal : EfEntityRepositoryBase<Log, DtContext>, ILogsDal
    {
        public Log Ekle(Log Log)
        {
            using (DtContext context = new DtContext())
            {
                context.Logs.Add(Log);

                context.SaveChanges();

                return Log;
            }
        }

        public IEnumerable<Log> GetAll()
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Logs.ToList();

                return result;
            }
        }


        public Log GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Logs.Where(a => a.Id == id).FirstOrDefault();

                return result;
            }
        }

        public Log Guncelle(Log Log)
        {
            using (var context = new DtContext())
            {
                context.Logs.Attach(Log);

                context.Entry(Log).State = EntityState.Modified;

                context.SaveChanges();
            }

            return Log;
        }

        public bool Sil(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var Log = context.Logs.FirstOrDefault(i => i.Id == id);

                if (Log != null)
                {
                    context.Logs.Remove(Log);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
