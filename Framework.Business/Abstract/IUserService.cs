using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
namespace Framework.Business.Abstract
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        IEnumerable<User> GetirListeAktif();
        User GetByPasswordExists(string userName,string Password);
        User GetByUserNameAndPassword(string userName, string password);
        List<UserRoleItem> GetUserRoles(User user);
        bool UserExists(string username);
        UserDetail GetUsers(string userName, string passwor);
        UserDetail GetByDetailByUserId(int Id);
        User GetById(int Id);
        User GetByGuid(Guid guid);
        User Register(User user, string password);
        User Ekle(User user);
        User Guncelle(User user);
        bool Sil(int id);
        string CreatePasswordHash(string password);
    }
}
