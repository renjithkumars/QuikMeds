using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuikMeds.Startup))]
namespace QuikMeds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
