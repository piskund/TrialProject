using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Backup.Admin.WebApp.Startup))]
namespace Backup.Admin.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
