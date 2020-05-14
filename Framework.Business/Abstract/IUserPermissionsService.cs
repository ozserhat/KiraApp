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
        IEnumerable<User_Permission> GetirListeAktif();

        List<User_Permission> GetUserByPermissions(int UserId);

        User_Permission GetById(int id);

        bool GetUserPermissionExists(User_Permission userPermission);

        User_Permission Ekle(User_Permission userPermission);

        User_Permission Guncelle(User_Permission userPermission);

        bool Sil(int id);
    }
}
