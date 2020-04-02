using Framework.Core.DataAccess;
using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<UserRoleItem> GetUserRoles(User user);
        UserDetail GetById(int Id);
        UserDetail GetUsers(User user);
        bool UserExists(string username);
        User Register(User user, string password);
    }
}
