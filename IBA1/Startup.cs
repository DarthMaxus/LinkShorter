using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IBA1.Startup))]
namespace IBA1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
