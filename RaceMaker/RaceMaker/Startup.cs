using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RaceMaker.Startup))]
namespace RaceMaker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
