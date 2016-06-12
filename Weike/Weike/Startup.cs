using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Weike.Startup))]
namespace Weike
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
