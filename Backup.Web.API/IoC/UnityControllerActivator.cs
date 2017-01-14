using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Practices.Unity;

namespace Backup.Web.API.IoC
{
    public class UnityControllerActivator : IHttpControllerActivator
    {
        private readonly IHttpControllerActivator _baseActivator;
        private readonly IUnityContainer _unity;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityControllerActivator"/> class.
        /// </summary>
        /// <param name="unity">The unity.</param>
        /// <param name="baseActivator">The base activator.</param>
        public UnityControllerActivator(IUnityContainer unity, IHttpControllerActivator baseActivator)
        {
            _unity = unity;
            _baseActivator = baseActivator;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Web.Http.Controllers.IHttpController" /> object.
        /// </summary>
        /// <param name="request">The message request.</param>
        /// <param name="controllerDescriptor">The HTTP controller descriptor.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        /// An <see cref="T:System.Web.Http.Controllers.IHttpController" /> object.
        /// </returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                GlobalConfiguration.Configuration.DependencyResolver.GetService(controllerType) as IHttpController;
            if (controller == null)
            {
                controller = _baseActivator.Create(request, controllerDescriptor, controllerType);
                if (controller != null) _unity.BuildUp(controller.GetType(), controller);
            }
            return controller;
        }
    }
}