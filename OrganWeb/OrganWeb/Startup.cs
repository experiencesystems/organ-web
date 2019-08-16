using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrganWeb.Startup))]
namespace OrganWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
