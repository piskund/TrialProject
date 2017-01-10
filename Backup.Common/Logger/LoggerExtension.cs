using System;

namespace Backup.Common.Logger
{
    public static class LoggerExtension
    {
        public static void LogActivity(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Activity, message));
        }

        public static void LogException(this ILogger logger, Exception e)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, e.Message, e));
        }
    }
}