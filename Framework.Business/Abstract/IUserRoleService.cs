using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IUserRoleService
    {
        IEnumerable<User_Role> GetAll();
        IEnumerable<User_Role> GetirListeAktif();
        User_Role GetById(int id);

        User_Role GetByUserId(int UserId);

        User_Role GetByRoleId(int RoleId);

        bool GetUserRoleExists(User_Role userRole);

        User_Role Ekle(User_Role userRole);

        User_Role Guncelle(User_Role userRole);

        bool Sil(int id);
    }
}
