using Ninject.Modules;

namespace Framework.Business.DependencyResolvers.Ninject
{
    public class ServiceModule:NinjectModule
    {
        public override void Load()
        {
            //Bind<IProductService>().ToConstant(WcfProxy<IProductService>.CreateChannel());
        }
    }
}
