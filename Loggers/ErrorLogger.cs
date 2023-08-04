using Serilog.Events;
using Serilog;

namespace ProductApi.Loggers
{
    public class ErrorLogger
    {
        public static Serilog.Core.Logger WriteLogToFile(IConfiguration configuration, string filePath)
        {
            var logger = new LoggerConfiguration().WriteTo.File(filePath, LogEventLevel.Error).CreateLogger();
            return logger;
        }
    }
}
