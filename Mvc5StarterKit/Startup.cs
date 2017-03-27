using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc5StarterKit.Startup))]
namespace Mvc5StarterKit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
