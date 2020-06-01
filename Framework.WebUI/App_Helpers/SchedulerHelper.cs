using Framework.Business.Abstract;
using Framework.Business.Concrete.Managers;
using Ninject;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;
namespace Framework.WebUI.App_Helpers
{

    public class SchedulerHelper
    {

        public static void RunJob()
        {
            try
            {
                var kernel = Business.DependencyResolvers.Ninject.InstanceFactory.GetirJobKernel();
                var scheduler = kernel.Get<IScheduler>();
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

                //IScheduler scheduler = schedulerFactory.GetScheduler();

                if (!scheduler.IsStarted)
                    scheduler.Start();


                IJobDetail job = JobBuilder.Create<HatirlaticiManager>().WithIdentity("myJob", "group1")
                    .Build();
                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                      (s =>
                         s
                        .OnEveryDay() //hergün çalışacağı bilgisi
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(17, 40)) //hergün hangi saatte çalışacağı bilgisi
                      )
                     .Build();
                //ITrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                //  .StartNow()
                //  .WithIdentity("HatirlaticiManager", "group1")
                //  .WithSimpleSchedule(x => x
                //  .WithIntervalInSeconds(1).RepeatForever())
                //  .Build();
                //scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex) { }
        }
    }
}