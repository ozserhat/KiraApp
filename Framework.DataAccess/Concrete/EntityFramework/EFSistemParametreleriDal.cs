﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Framework.Entities.Concrete;
using Framework.DataAccess.Abstract;
using Framework.Core.DataAccess.EntityFramework;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfSistemParametreleriDal : EfEntityRepositoryBase<SistemParametreleri, DtContext>, ISistemParametreleriDal
    {
        public SistemParametreleri GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.SistemParametreleri.Where(gta => gta.Id == id).FirstOrDefault();
            }
        }

        public SistemParametreleri GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                return context.SistemParametreleri.Where(gta => gta.Guid == guid).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var tur = context.SistemParametreleri.FirstOrDefault(i => i.Id == id);

                if (tur != null)
                {
                    context.SistemParametreleri.Remove(tur);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
