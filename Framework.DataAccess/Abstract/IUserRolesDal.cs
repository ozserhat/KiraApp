using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IUserRolesDal : IEntityRepository<User_Role>
    {
        IEnumerable<User_Role> GetAll();

        User_Role GetById(int id);

        User_Role GetByRoleId(int RoleId);

        User_Role GetByUserId(int UserId);

        User_Role GetUserRoleExists(User_Role userRole);

        User_Role Ekle(User_Role userRole);

        User_Role Guncelle(User_Role userRole);

        bool Sil(int id);
    }
}
