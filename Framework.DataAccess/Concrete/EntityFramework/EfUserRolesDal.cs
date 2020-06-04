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
    public class EfUserRolesDal : EfEntityRepositoryBase<User_Role, DtContext>, IUserRolesDal
    {
        public User_Role Ekle(User_Role userRole)
        {
            using (DtContext context = new DtContext())
            {
                context.User_Roles.Add(userRole);
                context.SaveChanges();
                return userRole;
            }
        }

        public IEnumerable<User_Role> GetAll()
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Roles
                             .Include(u=>u.Users).Include(r=>r.Roles).ToList();

                return result;
            }
        }

        public User_Role GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Roles.Where(a => a.Id == id).FirstOrDefault();

                return result;
            }
        }

        public User_Role GetByRoleId(int RoleId)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Roles.Where(a => a.Role_Id == RoleId).FirstOrDefault();

                return result;
            }
        }

        public User_Role GetByUserId(int UserId)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Roles.Where(a => a.User_Id == UserId).FirstOrDefault();

                return result;
            }
        }

        public User_Role GetUserRoleExists(User_Role userRole)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Roles.Where(a => a.User_Id == userRole.User_Id&&a.Role_Id==userRole.Role_Id&&a.IsDeleted==false).FirstOrDefault();

                return result;
            }
        }

        public User_Role Guncelle(User_Role userRole)
        {
            using (var context = new DtContext())
            {
                context.User_Roles.Attach(userRole);

                context.Entry(userRole).State = EntityState.Modified;

                context.SaveChanges();
            }

            return userRole;
        }

        public bool Sil(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var role = context.User_Roles.FirstOrDefault(i => i.Id == id);

                if (role != null)
                {
                    context.User_Roles.Remove(role);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }
    }
}
