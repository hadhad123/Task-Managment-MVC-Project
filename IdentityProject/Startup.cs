using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityProject.Startup))]
namespace IdentityProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
