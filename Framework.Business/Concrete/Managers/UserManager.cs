using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Framework.Business.Abstract;
using Framework.Core.Aspects.Postsharp.LogAspects;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Framework.DataAccess.Abstract;
using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;

namespace Framework.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User GetByUserNameAndPassword(string userName, string password)
        {
            var usr = _userDal.Get(u => u.UserName == userName & u.Password == password);

            if (usr is null)
                return null;

            //if (!VerifyPasswordHash(password, usr.PasswordHash, usr.PasswordSalt))
            //    return null;

            return usr;
        }

        public List<UserRoleItem> GetUserRoles(User user)
        {
            return _userDal.GetUserRoles(user);
        }

        public bool UserExists(string username)
        {
            if (_userDal.UserExists(username))
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordHash = hmac.Key;
                passwordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }

                return true;
            }
        }

        public User Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userDal.Register(user, password);

            return user;
        }

        public UserDetail GetUsers(string userName, string password)
        {
            var usr = _userDal.Get(u => u.UserName == userName & u.Password == password);

            if (usr is null)
                return null;

            var userDetail = _userDal.GetUsers(usr);

            return userDetail;
        }

        public UserDetail GetById(int Id)
        {
            var userDetail = _userDal.GetById(Id);

            return userDetail;
        }
    }
}
