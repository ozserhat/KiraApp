using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IControllerActionsDal
    {
        IEnumerable<ControllerAction> GetAll();

        ControllerAction GetById(int id);

        ControllerAction GetByGuid(Guid guid);

        ControllerAction GetExistsAndSave(IEnumerable<ControllerAction> controller);

        ControllerAction Add(ControllerAction controllerAction);

        void AddList(IEnumerable<ControllerAction> entities);

        ControllerAction Update(ControllerAction controllerAction);

    }
}
