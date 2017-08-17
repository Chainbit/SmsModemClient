using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmsModemServer.Startup))]
namespace SmsModemServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
