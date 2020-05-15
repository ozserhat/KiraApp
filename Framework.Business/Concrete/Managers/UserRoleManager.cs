using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class UserRoleManager : IUserRoleService
    {
        private IUserRolesDal _userRolesDal;

        public UserRoleManager(IUserRolesDal userRolesDal)
        {
            _userRolesDal = userRolesDal;
        }

        public User_Role Ekle(User_Role userRole)
        {
            return _userRolesDal.Ekle(userRole);
        }

        public IEnumerable<User_Role> GetAll()
        {
            return _userRolesDal.GetAll();
        }
        public User_Role GetById(int id)
        {
            return _userRolesDal.GetById(id);
        }

        public User_Role GetByRoleId(int RoleId)
        {
            return _userRolesDal.GetByRoleId(RoleId);
        }

        public User_Role GetByUserId(int UserId)
        {
            return _userRolesDal.GetById(UserId);
        }

        public bool GetUserRoleExists(User_Role userRole)
        {
            var result = _userRolesDal.GetUserRoleExists(userRole);

            if(result is null)
            return false;

            return true;
        }

        public User_Role Guncelle(User_Role userRole)
        {
            return _userRolesDal.Guncelle(userRole);
        }

        public bool Sil(int id)
        {
            return _userRolesDal.Sil(id);
        }
        public IEnumerable<User_Role> GetirListeAktif()
        {
            return _userRolesDal.GetAll().Where(a => a.IsDeleted == false);
        }
    }
}
