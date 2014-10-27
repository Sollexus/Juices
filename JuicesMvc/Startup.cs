using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JuicesMvc.Startup))]
namespace JuicesMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
