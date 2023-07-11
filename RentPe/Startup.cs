using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(RentPe.Startup))]
namespace RentPe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();

        }

       
    }
}
