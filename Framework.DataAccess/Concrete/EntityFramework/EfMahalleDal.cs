﻿using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Framework.DataAccess.Concrete.EntityFramework
{
  public  class EfMahalleDal : EfEntityRepositoryBase<Mahalle, DtContext>, IMahalleDal
    {
        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var Mahalle = context.Mahalleler.FirstOrDefault(i => i.Id == id);

                if (Mahalle != null)
                {
                    context.Mahalleler.Remove(Mahalle);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }



        public Mahalle GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                return context.Mahalleler.Include(x => x.Ilceler).Where(gta => gta.Id == id).FirstOrDefault();
            }
        }



        public IEnumerable<Mahalle> GetirListe()
        {
            using (DtContext context = new DtContext())
            {
                return context.Mahalleler.Include(x => x.Ilceler).ToList();
            }
        }
    }
}

