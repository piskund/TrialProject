using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backup.Common.Logger;

namespace Backup.Client.Console.App
{
    public class SomeClass
    {
        public SomeClass()
        { }

        public SomeClass(ILogger logger)
        {
            logger.Log(new LogEntry(LoggingEventType.Information , "aa"));
        }
    }
}
