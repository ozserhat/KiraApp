using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();

        Role GetById(int id);
        Role GetByGuid(Guid guid);
        Role Ekle(Role role);

        Role Guncelle(Role role);

        bool Sil(int id);
    }
}
