using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    public interface ILogger
    {
        void Debug(object logMessage);
        void Error(object logMessage);
        void Fatal(object logMessage);
        void Info(object logMessage);
        void Warn(object logMessage);

        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
    }
}
