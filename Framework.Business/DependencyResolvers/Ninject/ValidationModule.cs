using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Business.ValidationRules.FluentValidation;
using Framework.Entities.Concrete;
using FluentValidation;
using Ninject.Modules;

namespace Framework.Business.DependencyResolvers.Ninject
{
    public class ValidationModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<Role>>().To<RoleValidator>().InSingletonScope();
        }
    }
}
