using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mileage_Logger.Startup))]
namespace Mileage_Logger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
