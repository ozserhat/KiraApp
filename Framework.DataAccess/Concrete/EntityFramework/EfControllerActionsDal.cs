﻿using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfControllerActionsDal : EfEntityRepositoryBase<Role, DtContext>, IControllerActionsDal
    {
        public ControllerAction Add(ControllerAction controllerAction)
        {
            using (DtContext context = new DtContext())
            {
                context.ControllerActions.Add(controllerAction);

                context.SaveChanges();

                return controllerAction;
            }
        }

        public void AddList(IEnumerable<ControllerAction> entities)
        {
            using (DtContext context = new DtContext())
            {
                foreach (var entity in entities)
                    context.ControllerActions.Add(entity);

                context.SaveChanges();
            }
        }

        public IEnumerable<ControllerAction> GetAll()
        {
            using (DtContext context = new DtContext())
            {
                var result = context.ControllerActions.Where(r => r.IsDeleted == false).ToList();

                return result;
            }
        }

        public ControllerAction GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.ControllerActions.Where(a => a.Guid == guid).FirstOrDefault();

                return result;
            }
        }

        public ControllerAction GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.ControllerActions.Where(a => a.Id == id).FirstOrDefault();

                return result;
            }
        }

        public ControllerAction GetExistsAndSave(IEnumerable<ControllerAction> controller)
        {
            ControllerAction entity = null;

            using (DtContext context = new DtContext())
            {
                foreach (var item in controller)
                {
                    entity = context.ControllerActions.Where(a => a.Controller == item.Controller && a.Action == item.Action).FirstOrDefault();

                    if (entity is null)
                        entity = Add(item);
                }

                return entity;
            }
        }

        public ControllerAction Update(ControllerAction controllerAction)
        {
            using (var context = new DtContext())
            {
                context.ControllerActions.Attach(controllerAction);

                context.Entry(controllerAction).State = EntityState.Modified;

                context.SaveChanges();
            }

            return controllerAction;
        }
    }
}
