using Framework.Core.DataAccess.EntityFramework;
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
    public class EfRolesDal : EfEntityRepositoryBase<Role, DtContext>, IRolesDal
    {
        public Role Ekle(Role role)
        {
            using (DtContext context = new DtContext())
            {
                context.Roles.Add(role);

                context.SaveChanges();

                return role;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Roles.ToList();

                return result;
            }
        }

        public Role GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Roles.Where(a=>a.Id==id).FirstOrDefault();

                return result;
            }
        }

        public Role Guncelle(Role role)
        {
            using (var context = new DtContext())
            {
                role = context.Roles.Add(role);
                context.SaveChanges();
            }

            return role;
        }

        public bool Sil(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var role = context.Roles.FirstOrDefault(i => i.Id == id);

                if (role != null)
                {
                    context.Roles.Remove(role);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
