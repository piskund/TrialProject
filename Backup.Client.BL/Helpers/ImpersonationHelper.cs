// -------------------------------------------------------------------------------------------------------------
//  ImpersonationHelper.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Backup.Common.Interfaces;
using CodeContracts;
using Microsoft.Win32.SafeHandles;

namespace Backup.Client.BL.Helpers
{
    /// <summary>
    ///     Allows to request an impersonation token.
    ///     Implemented according to microsoft recommendations
    ///     https://msdn.microsoft.com/en-us/library/w070t6ka(v=vs.110).aspx
    /// </summary>
    public static class ImpersonationHelper
    {
        private const int Logon32LogonWithExistingCredentials = 2;
        private const int Logon32LogonWithNewCredentials = 9;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        ///     Gets the impersonation token.
        /// </summary>
        /// <param name="credentialInfo">The credential information.</param>
        /// <returns></returns>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static SafeTokenHandle GetImpersonationToken(this ICredentialInfo credentialInfo, bool isCurrentSystemLogon)
        {
            Requires.NotNull(credentialInfo, nameof(credentialInfo));
            Requires.NotNullOrEmpty(credentialInfo.UserName, nameof(credentialInfo.UserName));
            Requires.NotNullOrEmpty(credentialInfo.Password, nameof(credentialInfo.Password));
            return GetImpersonationToken(credentialInfo.UserName, credentialInfo.Password, isCurrentSystemLogon);
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword,
            int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);

        /// <summary>
        ///     Gets the impersonation token.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static SafeTokenHandle GetImpersonationToken(string userName, string password, bool isCurrentSystemLogon)
        {
            SafeTokenHandle safeTokenHandle;
            const int logon32ProviderDefault = 0;

            // Destination machine could not have user existing in source system, so we need to use new logon type in this case.
            int logonType = isCurrentSystemLogon ? Logon32LogonWithExistingCredentials : Logon32LogonWithNewCredentials;

            // Call LogonUser to obtain a handle to an access token.
            var returnValue = LogonUser(
                userName, Environment.UserDomainName, password,
                logonType, logon32ProviderDefault,
                out safeTokenHandle);

            if (false == returnValue)
            {
                var ret = Marshal.GetLastWin32Error();
                throw new Win32Exception(ret);
            }

            return safeTokenHandle;
        }

        /// <summary>
        ///     Impersonation token.
        /// </summary>
        /// <seealso cref="Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid" />
        public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeTokenHandle()
                : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                return CloseHandle(handle);
            }

            [DllImport("kernel32.dll")]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [SuppressUnmanagedCodeSecurity]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool CloseHandle(IntPtr handle);
        }
    }
}