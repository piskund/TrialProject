// -------------------------------------------------------------------------------------------------------------
//  BaseEntityFrameworkRepository.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.DAL.Repositories.EntityFramework;

namespace Backup.DAL.EntityFramework.Repositories
{
    public abstract class BaseEntityFrameworkRepository : IDisposable
    {
        protected BaseEntityFrameworkRepository() : this(new BackupContext())
        {
        }

        protected BaseEntityFrameworkRepository(BackupContext backupContext)
        {
            BackupContext = backupContext;
        }

        /// <summary>
        /// The backup context
        /// </summary>
        protected BackupContext BackupContext { get; }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    BackupContext.Dispose();
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