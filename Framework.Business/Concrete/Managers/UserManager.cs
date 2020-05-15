using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Framework.Business.Abstract;
//using Framework.Core.Aspects.Postsharp.LogAspects;
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
        public IEnumerable<User> GetAll()
        {
            return _userDal.GetAll().Where(a => a.IsDeleted == false);
        }

        public User GetByPasswordExists(string userName,string password)
        {
            string outPass = "";

            string passwordHash = CreatePasswordHash(password);

            var usr = _userDal.Get(u => u.UserName == userName && u.Password == passwordHash);

            if (usr is null)
                return null;

            outPass = VerifyPasswordHash(passwordHash, usr.PasswordSalt);

            if (outPass != password)
                return null;

            return usr;
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

        public string CreatePasswordHash(string password)
        {

            string passwordSalt = "b14ca5898a4e4133bbce2ea2315a1916";

            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(passwordSalt);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(password);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private string VerifyPasswordHash(string password, string passwordSalt)
        {
            var passwordOut = "";

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(password);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(passwordSalt);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            passwordOut = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return passwordOut;
        }

        public User Register(User user, string password)
        {
            //byte[] passwordHash, passwordSalt;

            //CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            //_userDal.Register(user, password);

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

        public UserDetail GetByDetailByUserId(int Id)
        {
            var userDetail = _userDal.GetByDetailByUserId(Id);

            return userDetail;
        }

        public User Ekle(User user)
        {
            return _userDal.Ekle(user);
        }

        public User Guncelle(User user)
        {
            return _userDal.Guncelle(user);
        }

        public bool Sil(int id)
        {
            return _userDal.Sil(id);
        }

        public User GetByGuid(Guid guid)
        {
            return _userDal.GetByGuid(guid);
        }

        public User GetById(int Id)
        {
            return _userDal.GetById(Id);
        }

        public IEnumerable<User> GetirListeAktif()
        {
            return _userDal.GetAll().Where(a => a.IsDeleted == false);
        }
    }
}
