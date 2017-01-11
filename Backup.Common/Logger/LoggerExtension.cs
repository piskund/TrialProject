using System;

namespace Backup.Common.Logger
{
    public static class LoggerExtension
    {
        public static void LogInfo(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message));
        }

        public static void LogException(this ILogger logger, Exception e)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, e.Message, e));
        }
    }
}