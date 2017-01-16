using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Backup.Admin.WebApp.Controllers;
using Backup.Admin.WebApp.IoC;
using Backup.DAL.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using UnityDependencyResolver = Microsoft.Practices.Unity.Mvc.UnityDependencyResolver;

namespace Backup.Admin.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            // Unity initialization.
            var unityContainer = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
            DependencyResolver.SetResolver(new UnityDependencyResolver(unityContainer));
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(unityContainer));
            unityContainer.LoadConfiguration();
            var repository = ServiceLocator.Current.GetInstance<IScheduledBackupRepository>();
            unityContainer.RegisterInstance<IController>("Home", new HomeController(repository));
        }
    }
}
