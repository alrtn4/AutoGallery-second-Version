using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SazeNegar.Web.Startup))]
namespace SazeNegar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
