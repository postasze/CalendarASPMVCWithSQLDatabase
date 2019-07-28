using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalendarWithBaseASPMVC.Startup))]
namespace CalendarWithBaseASPMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
