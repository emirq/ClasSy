using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClasSy.Startup))]
namespace ClasSy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
