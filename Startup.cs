using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerDataRatingsAndReviewsManagementSystem.Startup))]
namespace CustomerDataRatingsAndReviewsManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //signalr - userdefined
            app.MapSignalR();
        }
    }
}
