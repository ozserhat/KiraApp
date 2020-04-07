using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IControllerActionService
    {
        IEnumerable<ControllerAction> GetAll();

        ControllerAction GetById(int id);

        ControllerAction GetByGuid(Guid guid);
        ControllerAction GetByName(string ControllerName, string ActionName);
        ControllerAction Add(ControllerAction controller);
        ControllerAction GetExistsAndSave(IEnumerable<ControllerAction> controller);

        void AddList(IEnumerable<ControllerAction> entities);

        ControllerAction Update(ControllerAction controller);
    }
}
