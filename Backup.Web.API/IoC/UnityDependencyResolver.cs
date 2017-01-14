using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace Backup.Web.API.IoC
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly LinkedList<IUnityContainer> _children = new LinkedList<IUnityContainer>();
        private readonly IUnityContainer _unity;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="unity">The unity.</param>
        public UnityDependencyResolver(IUnityContainer unity)
        {
            _unity = unity;
        }

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            var child = _unity.CreateChildContainer();
            _children.AddLast(child);
            return new UnityDependencyResolver(child);
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>
        /// The retrieved service.
        /// </returns>
        public object GetService(Type serviceType)
        {
            try
            {
                var svc = _unity.Resolve(serviceType);
                return svc;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>
        /// The retrieved collection of services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                var svcs = _unity.ResolveAll(serviceType);
                return svcs;
            }
            catch
            {
                return Enumerable.Empty<object>();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var child in _children) child.Dispose();
            _children.Clear();
        }
    }
}