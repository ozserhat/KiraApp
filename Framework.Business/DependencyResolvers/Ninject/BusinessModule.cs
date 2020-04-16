using System.Data.Entity;
using Ninject.Modules;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Entities.Concrete;
using Framework.Core.DataAccess;
using Framework.Core.DataAccess.EntityFramework;
using Framework.Business.Abstract;
using Framework.Business.Concrete.Managers;
using Framework.DataAccess.Abstract;
using Framework.DataAccess.Concrete.EntityFramework;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net;
using log4net;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Framework.DataAccess.Concrete;

namespace Framework.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<EfUserDal>().InSingletonScope();

            Bind<IRoleService>().To<RoleManager>().InSingletonScope();
            Bind<IRolesDal>().To<EfRolesDal>().InSingletonScope();

            Bind<IUserRoleService>().To<UserRoleManager>().InSingletonScope();
            Bind<IUserRolesDal>().To<EfUserRolesDal>().InSingletonScope();

            Bind<IDenemeService>().To<DenemeManager>().InSingletonScope();
            Bind<IDenemeDal>().To<EfDenemeDal>().InSingletonScope();

            Bind<IGayrimenkulService>().To<GayrimenkulManager>().InSingletonScope();
            Bind<IGayrimenkulDal>().To<EfGayrimenkulDal>().InSingletonScope();

            Bind<IGayrimenkulTurService>().To<GayrimenkulTurManager>().InSingletonScope();
            Bind<IGayrimenkulTurDal>().To<EfGayrimenkulTurDal>().InSingletonScope();

            Bind<IGayrimenkulAlt_TurService>().To<GayrimenkulAlt_TurManager>().InSingletonScope();
            Bind<IGayrimenkulAlt_TurDal>().To<EfGayrimenkulAlt_TurDal>().InSingletonScope();

            Bind<IGayrimenkulDosya_TurService>().To<GayrimenkulDosya_TurManager>().InSingletonScope();
            Bind<IGayrimenkulDosya_TurDal>().To<EfGayrimenkulDosya_TurDal>().InSingletonScope();

            Bind<IControllerActionService>().To<ControllerActionManager>().InTransientScope();
            Bind<IControllerActionsDal>().To<EfControllerActionsDal>().InTransientScope();

            Bind<IUserPermissionsService>().To<UserPermissionManager>().InTransientScope();
            Bind<IUserPermissionsDal>().To<EfUserPermissionsDal>().InTransientScope();

            Bind<ILogsDal>().To<EfLogDal>().InTransientScope();
            Bind<ILogService>().To<LogsManager>().InTransientScope();

            Bind<IDuyuruDal>().To<EfDuyuruDal>().InTransientScope();
            Bind<IDuyuruService>().To<DuyuruManager>().InTransientScope();

            Bind<IDuyuru_TurDal>().To<EfDuyuru_TurDal>().InTransientScope();
            Bind<IDuyuru_TurService>().To<Duyuru_TurManager>().InTransientScope();

            Bind<IDuyuru_BildirimDal>().To<EfDuyuru_BildirimDal>().InTransientScope();
            Bind<IDuyuru_BildirimService>().To<Duyuru_BildirimManager>().InTransientScope();

            Bind<ISistemParametreleriService>().To<SistemParametreleriManager>().InTransientScope();
            Bind<ISistemParametreleriDal>().To<EfSistemParametreleriDal>().InTransientScope();

            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));
            Bind<DbContext>().To<DtContext>();

            Bind<ILog>().ToMethod(x => LogManager.GetLogger(typeof(DatabaseLogger)))
               .InSingletonScope();

            Bind<ILogger>().ToConstructor(ctorArg => new LoggerService(ctorArg.Inject<ILog>()));
            //Bind<NHibernateHelper>().To<SqlServerHelper>();
        }
    }
}
