using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelDeBotel.Startup))]
namespace HotelDeBotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
