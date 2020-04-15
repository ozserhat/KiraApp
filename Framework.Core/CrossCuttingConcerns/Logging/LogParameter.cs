using System;
using log4net;
using System.Collections.Generic;

namespace Framework.Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Url { get; set; }
        public string LogType { get; set; }
        public string HttpType { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
    }
}
