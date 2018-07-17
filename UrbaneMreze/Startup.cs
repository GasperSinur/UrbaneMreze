using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UrbaneMreze.Startup))]
namespace UrbaneMreze
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
