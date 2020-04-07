﻿using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IUserPermissionsDal : IEntityRepository<User_Permission>
    {
        IEnumerable<User_Permission> GetAll();
        User_Permission GetById(int id);

        User_Permission GetByPermissionControllerId(int id,int userid);

        List<User_Permission> GetUserByPermissions(int UserId);
        User_Permission GetUserPermissionExists(User_Permission userPermission);

    }
}
