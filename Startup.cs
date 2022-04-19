using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevPath.Startup))]
namespace DevPath
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}