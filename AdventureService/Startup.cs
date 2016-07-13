using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AdventureService.Startup))]

namespace AdventureService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}