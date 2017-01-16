// -------------------------------------------------------------------------------------------------------------
//  UnityControllerFactory.cs created by DEP on 2017/01/16
// -------------------------------------------------------------------------------------------------------------

using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Backup.Admin.WebApp.IoC
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _unity;

        public UnityControllerFactory(IUnityContainer unity)
        {
            this._unity = unity;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = _unity.Resolve<IController>(controllerName);
            if (controller == null)
            {
                controller = base.CreateController(requestContext, controllerName);
            }
            if (controller != null)
            {
                _unity.BuildUp(controller.GetType(), controller);
            }
            return controller;
        }
    }
}