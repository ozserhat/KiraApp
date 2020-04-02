using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models.ViewModels
{
    public class LoginVm : VMBase
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}