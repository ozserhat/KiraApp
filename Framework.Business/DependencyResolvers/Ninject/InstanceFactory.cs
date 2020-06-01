using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.DataAccess;
using Framework.Core.DataAccess.EntityFramework;
using Framework.Business.Abstract;
using Framework.Business.Concrete.Managers;
using Framework.DataAccess.Abstract;
using Framework.DataAccess.Concrete.EntityFramework;
using Ninject;
using Ninject.Modules;
using Quartz;
using Quartz.Impl;

namespace Framework.Business.DependencyResolvers.Ninject
{
    public class InstanceFactory
    {
        public static IKernel GetirJobKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IScheduler>().ToMethod(x =>
            {
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                var sched = schedulerFactory.GetScheduler();
                sched.JobFactory = new NinjectJobFactory(kernel);
                return sched;
            });

            kernel.Bind<IJob>().To<HatirlaticiManager>();
            kernel.Bind<IHatirlaticiService>().To<HatirlaticiManager>();
            kernel.Bind<ITahakkukDal>().To<EfTahakkukDal>();
            return kernel;
        }

        public static T GetInstance<T>()
        {
            var kernel = new StandardKernel(new BusinessModule(), new AutoMapperModule());
           

            return kernel.Get<T>();
        }
    }
}
