using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(curd.Startup))]
namespace curd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
