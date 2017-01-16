// -------------------------------------------------------------------------------------------------------------
//  BackupInfoViewModel.cs created by DEP on 2017/01/16
// -------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Backup.Common.Entities;

namespace Backup.Admin.WebApp.Models
{
    public class BackupInfoViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackup"/> class.
        /// Default consturctor.
        /// </summary>
        public BackupInfoViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackup"/> class.
        /// Copy constructor.
        /// </summary>
        /// <param name="scheduledBackup">The customer.</param>
        public BackupInfoViewModel(ScheduledBackup scheduledBackup)
        {
            Id = scheduledBackup.Id;
            BackupConfigId = scheduledBackup.BackupConfig.Id;
            ScheduledDate = scheduledBackup.ScheduledDateTime;
            IpAddress = scheduledBackup.BackupConfig.ClientIpAddress;
            SourceFolderPath = scheduledBackup.BackupConfig.SourceFolderPath;
            DestinationFolderPath = scheduledBackup.BackupConfig.DestinationFolderPath;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int BackupConfigId { get; set; }

        [Display(Name = "Scheduled start")]
        public DateTime ScheduledDate { get; set; }

        [Display(Name = "Ip address")]
        public string IpAddress { get; set; }

        [Display(Name = "Backup source path")]
        public string SourceFolderPath { get; set; }

        [Display(Name = "Backup destination path")]
        public string DestinationFolderPath { get; set; }

        public static explicit operator BackupInfoViewModel(ScheduledBackup entity)
        {
            return new BackupInfoViewModel(entity);
        }
    }
}