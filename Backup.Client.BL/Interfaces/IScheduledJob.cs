// -------------------------------------------------------------------------------------------------------------
//  IScheduledJob.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Defines executable job.
    /// </summary>
    public interface IScheduledJob
    {
        /// <summary>
        /// Gets the scheduled date time.
        /// </summary>
        /// <value>
        /// The scheduled date time.
        /// </value>
        DateTime ScheduledDateTime { get; }

        /// <summary>
        /// Executes this job.
        /// </summary>
        void Execute();
    }
}