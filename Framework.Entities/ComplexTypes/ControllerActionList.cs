﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.ComplexTypes
{
    public class ControllerActionList
    {
        public string AreaName{ get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Attributes { get; set; }
        public string ReturnType { get; set; }
    }
}
