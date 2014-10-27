using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JuicesWebForms.Startup))]
namespace JuicesWebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
