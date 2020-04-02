using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Framework.Core.DataAccess.EntityFramework;
using Framework.DataAccess.Abstract;
using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DtContext>, IUserDal
    {
        public UserDetail GetById(int Id)
        {
            UserDetail userDetail = null;

            using (DtContext context = new DtContext())
            {
                var result = context.UserRoles
                               .Include(x => x.Roles)
                               .Include(x => x.Users)
                               .Where(x => x.UserId==Id)
                               .ToList();

                if (result != null)
                {
                    userDetail = new UserDetail()
                    {
                        RoleId = result.FirstOrDefault().RoleId,
                        UserId = result.FirstOrDefault().UserId,
                        RoleName = result.FirstOrDefault().Roles.Name,
                        UserRoles= context.UserRoles.Where(ur => ur.UserId == Id).ToList(),
                        User = result.FirstOrDefault().Users
                    };
                }

                return userDetail;
            }
        }

        public List<UserRoleItem> GetUserRoles(User user)
        {
            using (DtContext context = new DtContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles
                             on ur.UserId equals user.Id
                             where ur.UserId == user.Id
                             select new UserRoleItem { RoleName = r.Name };

                return result.ToList();
            }
        }

        public UserDetail GetUsers(User user)
        {
            UserDetail userDetail = null;

            using (DtContext context = new DtContext())
            {
                var result = context.UserRoles
                               .Include(x => x.Users)
                               .Include(x => x.Roles)
                               .Where(x => x.Users.UserName == user.UserName && x.Users.Password == user.Password)
                               .FirstOrDefault();

                if (result != null)
                {
                    userDetail = new UserDetail() {
                        RoleId = result.RoleId,
                        UserId=result.UserId,
                        RoleName=result.Roles.Name,
                        Role=result.Roles,
                        UserRoles = context.UserRoles.Where(ur => ur.UserId == user.Id).ToList(),
                        User = result.Users
                    };
                }

                return userDetail;
            }
        }

        public User Register(User user, string password)
        {
            using (DtContext context = new DtContext())
            {
                context.Users.Add(user);

                context.SaveChanges();

                return user;
            }
        }

        public bool UserExists(string username)
        {
            using (DtContext context = new DtContext())
            {
                if (context.Users.Any(u => u.UserName == username))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
