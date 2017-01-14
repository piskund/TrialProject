// -------------------------------------------------------------------------------------------------------------
//  Global.asax.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Backup.Web.API.IoC;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Backup.Web.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Configure DI (Unity)
            var unityContainer = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(unityContainer);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
                new UnityControllerActivator(unityContainer,
                    GlobalConfiguration.Configuration.Services.GetHttpControllerActivator()));
            unityContainer.LoadConfiguration();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}