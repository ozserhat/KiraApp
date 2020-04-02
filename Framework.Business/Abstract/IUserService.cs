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
        User GetByUserNameAndPassword(string userName, string password);
        List<UserRoleItem> GetUserRoles(User user);
        bool UserExists(string username);
        UserDetail GetUsers(string userName, string passwor);
        UserDetail GetById(int Id);
        User Register(User user, string password);
    }
}
