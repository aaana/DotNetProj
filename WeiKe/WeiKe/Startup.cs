using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeiKe.Startup))]
namespace WeiKe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
