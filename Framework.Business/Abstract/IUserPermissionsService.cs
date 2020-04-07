using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IUserPermissionsService
    {
        IEnumerable<User_Permission> GetAll();

        List<User_Permission> GetUserByPermissions(int UserId);

        User_Permission GetById(int id);

        bool GetUserPermissionExists(User_Permission userPermission);
    }
}
