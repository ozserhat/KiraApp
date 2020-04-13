using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
    }
}
