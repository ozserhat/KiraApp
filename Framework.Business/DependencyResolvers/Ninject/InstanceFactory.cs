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
namespace Framework.Business.DependencyResolvers.Ninject
{
    public class InstanceFactory
    {
        public static T GetInstance<T>()
        {
            var kernel = new StandardKernel(new BusinessModule(), new AutoMapperModule());
            return kernel.Get<T>();
        }
    }
}
