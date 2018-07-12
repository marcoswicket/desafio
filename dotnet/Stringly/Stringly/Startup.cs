using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stringly.Startup))]
namespace Stringly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
