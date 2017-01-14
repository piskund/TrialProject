// -------------------------------------------------------------------------------------------------------------
//  FakeLogger.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

namespace Backup.Common.Logger
{
    /// <summary>
    /// Default implementation of ILogger. Do nothingh, just stub.
    /// </summary>
    /// <seealso cref="Backup.Common.Logger.ILogger" />
    public class FakeLogger : ILogger
    {
        public void Log(LogEntry entry)
        {
        }
    }
}