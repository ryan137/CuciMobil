using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CuciMobilv4.Startup))]
namespace CuciMobilv4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
