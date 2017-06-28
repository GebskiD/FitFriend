using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitFriend.Startup))]
namespace FitFriend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
