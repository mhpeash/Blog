using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProblemsBlog.Startup))]
namespace ProblemsBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
