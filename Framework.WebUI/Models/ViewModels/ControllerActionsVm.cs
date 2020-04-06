using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models.ViewModels
{
    public class ControllerActionsVm : PagingVMBase
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Attributes { get; set; }
        public string ReturnType { get; set; }
        public IPagedList<ControllerAction> ControllerActionList { get; set; }
    }
}