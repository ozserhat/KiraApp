using log4net;

namespace Framework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DatabaseLogger:LoggerService
    {
        public DatabaseLogger() : base(LogManager.GetLogger("DatabaseLogger"))
        {
        }
    }
}
