using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Framework.Business.Concrete.Managers
{
    public class UserPermissionManager : IUserPermissionsService
    {
        private IUserPermissionsDal _userPermissionDal;

        public UserPermissionManager(IUserPermissionsDal userPermissionDal)
        {
            _userPermissionDal = userPermissionDal;
        }

        public User_Permission Ekle(User_Permission userPermission)
        {
            return _userPermissionDal.Add(userPermission);
        }

        public IEnumerable<User_Permission> GetAll()
        {
            return _userPermissionDal.GetAll();
        }
        public IEnumerable<User_Permission> GetirListeAktif()
        {
            return _userPermissionDal.GetList().Where(a => a.AktifMi == true);
        }
        public User_Permission GetById(int id)
        {
            return _userPermissionDal.GetById(id);
        }

        public List<User_Permission> GetUserByPermissions(int UserId)
        {
            return _userPermissionDal.GetUserByPermissions(UserId);
        }

        public bool GetUserPermissionExists(User_Permission userPermission)
        {
            var result = _userPermissionDal.GetUserPermissionExists(userPermission);

            if (result is null)
                return false;

            return true;
        }

        public User_Permission Guncelle(User_Permission userPermission)
        {
            return _userPermissionDal.Guncelle(userPermission);
        }

        public bool Sil(int id)
        {
            return _userPermissionDal.Delete(id);
        }
    }
}
