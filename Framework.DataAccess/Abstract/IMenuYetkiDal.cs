﻿using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IMenuYetkiDal : IEntityRepository<MenuYetki>
    {
        IEnumerable<MenuYetki> GetAll();
    }
}
