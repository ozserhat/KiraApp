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
    public class EfUserPermissionsDal : EfEntityRepositoryBase<User_Permission, DtContext>, IUserPermissionsDal
    {
        public IEnumerable<User_Permission> GetAll()
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Permissions
                                    .Include(u => u.Users)
                                    .Include(r => r.ControllerActions)
                                    .Where(r => r.AktifMi == true).ToList();

                return result;
            }
        }

        public User_Permission GetById(int id)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Permissions
                                    .Include(u => u.Users)
                                    .Include(r => r.ControllerActions)
                                    .Where(r => r.Id == id && r.AktifMi == true).FirstOrDefault();

                return result;
            }
        }

        public User_Permission GetByPermissionControllerId(int id, int userid)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Permissions
                                   .Include(u => u.Users)
                                   .Include(r => r.ControllerActions)
                                   .Where(r => r.ControllerAction_Id == id && r.User_Id == userid && r.AktifMi == true).FirstOrDefault();

                return result;
            }
        }

        public List<User_Permission> GetUserByPermissions(int UserId)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Permissions
                                    .Include(u => u.Users)
                                    .Include(r => r.ControllerActions)
                                    .Where(r => r.User_Id == UserId
                                           && r.AktifMi == true).ToList();

                return result;
            }
        }

        public User_Permission GetUserPermissionExists(User_Permission userPermission)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Permissions
                                    .Include(u => u.Users)
                                    .Include(r => r.ControllerActions)
                                    .Where(r => r.User_Id == userPermission.User_Id
                                    && r.ControllerAction_Id == userPermission.ControllerAction_Id
                                    && r.AktifMi == true).FirstOrDefault();

                return result;
            }
        }

        public User_Permission Guncelle(User_Permission userPermission)
        {
            using (var context = new DtContext())
            {
                context.User_Permissions.Attach(userPermission);

                context.Entry(userPermission).State = EntityState.Modified;

                context.SaveChanges();
            }

            return userPermission;
        }

        public bool Delete(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var yetki = context.User_Permissions.FirstOrDefault(i => i.Id == id);

                if (yetki != null)
                {
                    context.User_Permissions.Attach(yetki);

                    context.Entry(yetki).State = EntityState.Modified;

                    sonuc = true;
                }
            }

            return sonuc;
        }

        public User_Permission GetByVisibilityControllerId(int id, int userid)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Permissions
                                   .Include(u => u.Users)
                                   .Include(r => r.ControllerActions)
                                   .Where(r => r.ControllerAction_Id == id && r.User_Id == userid && r.GorulebilirMi == true).FirstOrDefault();

                return result;
            }
        }
    }
}
