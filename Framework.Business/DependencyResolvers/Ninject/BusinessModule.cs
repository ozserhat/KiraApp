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

namespace Framework.Business.DependencyResolvers.Ninject
{
    public class BusinessModule:NinjectModule
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

            Bind<IGayrimenkulTurService>().To<GayrimenkulTurManager>().InSingletonScope();
            Bind<IGayrimenkulTurDal>().To<EfGayrimenkulTurDal>().InSingletonScope();

            Bind<IControllerActionService>().To<ControllerActionManager>().InTransientScope();
            Bind<IControllerActionsDal>().To<EfControllerActionsDal>().InTransientScope();

            Bind<IUserPermissionsService>().To<UserPermissionManager>().InTransientScope();
            Bind<IUserPermissionsDal>().To<EfUserPermissionsDal>().InTransientScope();
            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));
            Bind<DbContext>().To<DtContext>();
            //Bind<NHibernateHelper>().To<SqlServerHelper>();
        }
    }
}
