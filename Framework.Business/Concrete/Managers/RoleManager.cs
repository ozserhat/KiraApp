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
    public class RoleManager : IRoleService
    {
        private IRolesDal _roleDal;

        public RoleManager(IRolesDal roleDal)
        {
            _roleDal = roleDal;
        }
        public Role Ekle(Role role)
        {
            return _roleDal.Ekle(role);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleDal.GetAll();
        }

        public Role GetById(int id)
        {
            return _roleDal.GetById(id);
        }

        public Role Guncelle(Role role)
        {
            return _roleDal.Guncelle(role);
        }

        public bool Sil(int id)
        {
            return _roleDal.Sil(id);
        }
    }
}
