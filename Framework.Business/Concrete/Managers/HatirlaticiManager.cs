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
    public class JobFactory : IJobFactory
    {
        readonly IKernel _kernel;

        private readonly IServiceProvider _serviceProvider;    
   
        public JobFactory(IKernel kernel, IServiceProvider serviceProvider)
        {
            _kernel = kernel;

            _serviceProvider = serviceProvider;
        }
 
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                // this will inject dependencies that the job requires
                return (IJob)this._kernel.Get(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                throw new SchedulerException(string.Format("Problem while instantiating job '{0}' from the NinjectJobFactory.", bundle.JobDetail.Key), e);
            }
        }

        public void ReturnJob(IJob job)
        {
            if (job is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    [DisallowConcurrentExecution]
    public class HatirlaticiManager : IHatirlaticiService, IJob
    {
        private ITahakkukDal _tahakkukDal;

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
