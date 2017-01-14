// -------------------------------------------------------------------------------------------------------------
//  IActivityTracker.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.Client.BL.BackupLogic;
using Backup.Common.DTO;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Defines tracked activity contract.
    /// </summary>
    public interface IActivityTracker
    {
        /// <summary>
        /// Gets the activity status.
        /// </summary>
        /// <value>
        /// The activity status.
        /// </value>
        ActivityStatusType ActivityStatus { get; }

        /// <summary>
        /// Occurs when [activity status changed].
        /// </summary>
        event EventHandler<ActivityChangedEventArgs> ActivityStatusChanged;
    }
}