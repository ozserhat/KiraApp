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
        public IEnumerable<User> GetAll()
        {
            using (DtContext context = new DtContext())
            {
                var users = context.Users.Include(u => u.User_Roles.Select(r => r.Roles)).ToList();

                return users;
            }
            //return null;
        }

        public UserDetail GetByDetailByUserId(int Id)
        {
            UserDetail userDetail = null;

            using (DtContext context = new DtContext())
            {
                var result = context.Users.Include(u => u.User_Roles.Select(r => r.Roles))
                               .Where(x => x.Id == Id)
                               .ToList();

                //if (result != null)
                //{
                //    userDetail = new UserDetail()
                //    {
                //        RoleId = result.FirstOrDefault().Role_Id,
                //        UserId = result.FirstOrDefault().User_Id,
                //        RoleName = result.FirstOrDefault().Roles.Name,
                //        UserRoles = context.User_Roles.Where(ur => ur.User_Id == Id).ToList(),
                //        User = result.FirstOrDefault().Users
                //    };
                //}
            }

            return userDetail;
        }

        public User GetById(int Id)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Users
                            .Include(u => u.User_Roles.Select(r => r.Roles))
                            .Where(a => a.Id == Id).FirstOrDefault();

                return result;
            }
        }

        public User GetByGuid(Guid guid)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.Users.Where(a => a.Guid == guid).FirstOrDefault();

                return result;
            }
        }

        public List<UserRoleItem> GetUserRoles(User user)
        {
            using (DtContext context = new DtContext())
            {
                var result = context.User_Roles
                            .Include(u => u.Roles)
                            .Where(ur => ur.User_Id == user.Id)
                            .Select(x=>new UserRoleItem { RoleName =x.Roles.Name});
                return result.ToList();
            }
            //return null;
        }

        public UserDetail GetUsers(User user)
        {
            UserDetail userDetail = null;

            using (DtContext context = new DtContext())
            {
                var result = context.User_Roles.Include(x => x.Users).Include(x => x.Roles)
                               .Where(x => x.Users.UserName == user.UserName && x.Users.Password == user.Password).OrderByDescending(a => a.Id)
                               .FirstOrDefault();

                if (result != null)
                {
                    userDetail = new UserDetail()
                    {
                        RoleId = result.Role_Id,
                        UserId = result.User_Id,
                        RoleName = result.Roles.Name,
                        Role = result.Roles,
                        UserRoles = context.User_Roles.Where(ur => ur.User_Id == user.Id).ToList(),
                        User = result.Users
                    };
                }

            }

            return userDetail;
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

        public User Ekle(User user)
        {
            using (DtContext context = new DtContext())
            {
                context.Users.Add(user);

                context.SaveChanges();

                return user;
            }
        }

        public User Guncelle(User user)
        {
            using (var context = new DtContext())
            {
                context.Users.Add(user);

                context.Entry(user).State = EntityState.Modified;

                context.SaveChanges();
            }

            return user;
        }

        public bool Sil(int id)
        {
            bool sonuc = false;

            using (var context = new DtContext())
            {
                var user = context.Users.FirstOrDefault(i => i.Id == id);

                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    sonuc = true;
                }
            }

            return sonuc;
        }

    }
}
