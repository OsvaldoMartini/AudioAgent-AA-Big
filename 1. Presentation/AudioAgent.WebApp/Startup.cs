using AudioAgent.WebApp;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("AudioAgent", typeof(Startup))]
namespace AudioAgent.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
