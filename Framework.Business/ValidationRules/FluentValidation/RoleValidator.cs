using FluentValidation;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.ValidationRules.FluentValidation
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Rol Adı Boş Bırakılamaz!!!");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Açıklama Boş Bırakılamaz!!!");
        }
    }
}
