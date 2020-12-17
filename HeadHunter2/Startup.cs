using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeadHunter2.Startup))]
namespace HeadHunter2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
