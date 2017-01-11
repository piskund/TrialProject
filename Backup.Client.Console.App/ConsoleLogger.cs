using Backup.Common.Logger;
using CodeContracts;
using static System.Console;

namespace Backup.Client.Console.App
{
    /// <summary>
    ///     Very simple implementation of logger. Basically in order to demonstrate a usage of IoC.
    /// </summary>
    /// <seealso cref="Backup.Common.Logger.ILogger" />
    internal class ConsoleLogger : ILogger
    {
        /// <summary>
        ///     Logs the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void Log(LogEntry entry)
        {
            Requires.NotNull(entry, nameof(entry));
            WriteLine(entry.Message);
            if (entry.Exception != null)
            {
                WriteLine($"{entry.Exception.GetType()} has been thrown! Stack: {entry.Exception.StackTrace}");
            }
        }
    }
}