using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentPe.Startup))]
namespace RentPe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }
    }
}
