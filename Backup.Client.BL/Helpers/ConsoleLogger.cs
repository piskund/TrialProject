using System;
using Backup.Common.Logger;
using CodeContracts;

namespace Backup.Client.BL.Helpers
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
            Console.WriteLine(entry.Message);
            if (entry.Exception != null)
            {
                Console.WriteLine($"{entry.Exception.GetType()} has been thrown! Stack: {entry.Exception.StackTrace}");
            }
        }
    }
}