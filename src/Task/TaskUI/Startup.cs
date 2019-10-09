using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskUI.Startup))]
namespace TaskUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
