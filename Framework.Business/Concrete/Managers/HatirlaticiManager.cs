using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using Framework.Entities.Enums;
using Ninject;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{

    [DisallowConcurrentExecution]
    public class HatirlaticiManager : IHatirlaticiService, IJob
    {
        private readonly ITahakkukDal _tahakkukDal;

        public HatirlaticiManager(ITahakkukDal tahakkukDal)
        {
            _tahakkukDal = tahakkukDal;
        }

        public void Execute(IJobExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Task taskA = new Task(() => Debug.WriteLine("Hello from task at {0}", DateTime.Now.ToString()));
            taskA.Start();
        }

        public Tahakkuk Getir(int id)
        {
            return _tahakkukDal.GetById(id);
        }

        public IEnumerable<Tahakkuk> GetirListeAktif()
        {
            return _tahakkukDal.GetirListe();
        }

        public IEnumerable<Tahakkuk> GetirOdenmeyenTahakkuklar()
        {
            return _tahakkukDal.GetirOdenmeyenTahakkuklar();
        }
    }
}
