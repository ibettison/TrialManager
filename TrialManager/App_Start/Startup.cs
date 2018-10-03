using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(Trialmanager.Startup))]
namespace Trialmanager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}