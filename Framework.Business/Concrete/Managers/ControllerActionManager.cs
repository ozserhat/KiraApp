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
    public class ControllerActionManager : IControllerActionService
    {
        private IControllerActionsDal _controllerActionsDal;

        public ControllerActionManager(IControllerActionsDal controllerActionsDal)
        {
            _controllerActionsDal = controllerActionsDal;
        }
        public ControllerAction Add(ControllerAction controller)
        {
            return _controllerActionsDal.Add(controller);
        }

        public void AddList(IEnumerable<ControllerAction> entities)
        {
           _controllerActionsDal.AddList(entities);
        }

        public IEnumerable<ControllerAction> GetAll()
        {
            return _controllerActionsDal.GetAll();
        }

        public ControllerAction GetByGuid(Guid guid)
        {
            return _controllerActionsDal.GetByGuid(guid);
        }

        public ControllerAction GetById(int id)
        {
            return _controllerActionsDal.GetById(id);
        }

        public ControllerAction GetByName(string ControllerName, string ActionName)
        {
            return _controllerActionsDal.GetByName(ControllerName,ActionName);
        }

        public ControllerAction GetExistsAndSave(IEnumerable<ControllerAction> controller)
        {
           return _controllerActionsDal.GetExistsAndSave(controller);
        }

        public ControllerAction Update(ControllerAction controller)
        {
            return _controllerActionsDal.Update(controller);
        }
    }
}
