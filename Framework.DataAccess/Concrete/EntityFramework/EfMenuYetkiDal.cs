﻿using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfMenuYetkiDal : EfEntityRepositoryBase<MenuYetki, DtContext>, IMenuYetkiDal
    {
        public IEnumerable<MenuYetki> GetAll()
        {
            using (DtContext context = new DtContext())
            {
                return context.MenuYetkileri.ToList();


            }
  
        }

    }
}
