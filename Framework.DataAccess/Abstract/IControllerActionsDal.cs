using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IControllerActionsDal : IEntityRepository<ControllerAction>
    {
        IEnumerable<ControllerAction> GetAll();

        ControllerAction GetById(int id);

        ControllerAction GetByName(string ControllerName,string ActionName);

        ControllerAction GetByGuid(Guid guid);

        ControllerAction GetExistsAndSave(IEnumerable<ControllerAction> controller);


        void AddList(IEnumerable<ControllerAction> entities);

    }
}
