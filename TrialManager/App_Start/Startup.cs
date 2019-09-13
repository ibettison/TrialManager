using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(TrialManager.Startup))]
namespace TrialManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}