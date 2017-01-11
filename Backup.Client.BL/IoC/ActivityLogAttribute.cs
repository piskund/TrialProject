﻿using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Backup.Client.BL.Unity
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ActivityLogAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ActivityLogHandler {Order = Order};
        }
    }
}