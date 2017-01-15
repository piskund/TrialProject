// -------------------------------------------------------------------------------------------------------------
//  BackupController.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Timers;
using Backup.Client.BL.Interfaces;
using Backup.Common.Logger;

namespace Backup.Client.BL.BackupLogic
{
    public class BackupController : IBackupController
    {
        // Request service every minute.
        private const int DefaultTimerInterval = 60000;
        private readonly IActivityFacade _activityFacade;
        private readonly IBackupFacade _backupFacade;

        private readonly ILogger _logger;
        private readonly Timer _requestTimer;

        public BackupController(IBackupFacade backupFacade, IActivityFacade activityFacade, ILogger logger)
        {
            _logger = logger;
            _requestTimer = new Timer(DefaultTimerInterval);
            _requestTimer.Elapsed += HandleTimer;
            _requestTimer.Start();
            _backupFacade = backupFacade;
            _activityFacade = activityFacade;
        }

        private static void HandleTimer(object source, ElapsedEventArgs args)
        {
            Console.WriteLine("Timer");
        }
    }
}