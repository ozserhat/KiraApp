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
                                    .Where(r => r.IsDeleted == false).ToList();

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
                                    .Where(r => r.Id == id && r.IsDeleted == false).FirstOrDefault();

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
                                   .Where(r => r.ControllerAction_Id == id && r.User_Id == userid && r.IsAuthorize == true && r.IsDeleted == false).FirstOrDefault();

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
                                           && r.IsDeleted == false).ToList();

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
                                    && r.IsDeleted == false).FirstOrDefault();

                return result;
            }
        }

    }
}
