// -------------------------------------------------------------------------------------------------------------
//  BackupController.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;
using Backup.Common.Logger;
using CodeContracts;
using Microsoft.Practices.ObjectBuilder2;

namespace Backup.Client.BL.BackupLogic
{
    public class BackupController : IListener, IDisposable
    {
        // Request the service every minute.
        private const int DefaultTimerInterval = 60000;

        private readonly IRemoteRequestsFacade _remoteRequestsFacade;
        private readonly IBackupStrategy _backupStrategy;
        private readonly ScheduledJobsManager _jobsManager;

        private readonly ILogger _logger;
        private readonly Timer _requestTimer;
        private IEnumerable<ScheduledBackup> _lastScheduledBackups = Enumerable.Empty<ScheduledBackup>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupController" /> class.
        /// </summary>
        /// <param name="remoteRequestsFacade">The remote requests facade.</param>
        /// <param name="backupStrategy">The backup strategy.</param>
        /// <param name="logger">The logger.</param>
        public BackupController(
            IRemoteRequestsFacade remoteRequestsFacade,
            IBackupStrategy backupStrategy,
            ILogger logger)
        {
            _logger = logger;
            _requestTimer = new Timer(DefaultTimerInterval);
            _remoteRequestsFacade = remoteRequestsFacade;
            _backupStrategy = backupStrategy;
            _jobsManager = new ScheduledJobsManager(_logger);
        }

        /// <summary>
        /// Starts the listening with default interval.
        /// </summary>
        public void StartListen()
        {
            if (!_requestTimer.Enabled)
            {
                StartListen(DefaultTimerInterval);
            }
        }

        /// <summary>
        /// Starts the listening of the backup provider with the given interval.
        /// </summary>
        /// <param name="timerInterval">The timer interval.</param>
        public void StartListen(int timerInterval)
        {
            if (!_requestTimer.Enabled)
            {
                _requestTimer.Interval = timerInterval;
                _logger.LogInfo($"Start listening web service with interval {_requestTimer.Interval / 1000} sec.");
                _requestTimer.Elapsed += async (sender, e) => await HandleTimer();
                _requestTimer.Start();
            }
            else
            {
                _logger.LogInfo("The listening has already started, attempts to start again will have no effect.");
            }
        }

        /// <summary>
        /// Handles the timer.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task HandleTimer()
        {
            var scheduledBackups = _remoteRequestsFacade.GetBackups();
            _logger.LogInfo($"Got {scheduledBackups.Count()} scheduled backups.");
            if (!_lastScheduledBackups.SequenceEqual(scheduledBackups))
            {
                _logger.LogInfo("Backups differ from previous ones, starting to reinitialize.");
                _lastScheduledBackups = new List<ScheduledBackup>(scheduledBackups);
                var scheduledJobs =
                    scheduledBackups.Select(scheduledBackup => new ScheduledBackupJob(scheduledBackup, _backupStrategy));
                scheduledJobs.ForEach(j => j.ActivityStatusChanged += OnJobActivityStatusChanged);
                await _jobsManager.ProceedScheduledJobsAsync(scheduledJobs);
            }
            else
            {
                _logger.LogInfo("Recieved backups list matches to the previous one.");
            }
        }

        /// <summary>
        /// Called when [job activity status changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ActivityChangedEventArgs"/> instance containing the event data.</param>
        private void OnJobActivityStatusChanged(object sender, ActivityChangedEventArgs e)
        {
            Requires.NotNull(e, nameof(e));
            Requires.NotNull(e.ScheduledBackup, nameof(e.ScheduledBackup));
            Requires.NotNull(e.ScheduledBackup, nameof(e.ActivityStatus));
            _logger.LogInfo($"Backup Id: {e.ScheduledBackup.Id}, Status: {e.ActivityStatus}");
            _remoteRequestsFacade.Save(new ActivityInfo(e.ScheduledBackup, e.ActivityStatus));
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _requestTimer.Stop();
                    _requestTimer.Close();
                    _jobsManager.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion
    }
}