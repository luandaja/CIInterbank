using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Interbank.Startup))]
namespace Interbank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
