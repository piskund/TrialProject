using System;
using CodeContracts;

namespace Backup.Common.Logger
{
    /// <summary>
    /// Simple DTO object contains information about log message.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public LoggingEventType Severity { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {
            Requires.NotNullOrEmpty(message, nameof(message));
            Severity = severity;
            Message = message;
            Exception = exception;
        }
    }
}