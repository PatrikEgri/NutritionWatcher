using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NutritionWatcher.Startup))]
namespace NutritionWatcher
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
