using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Area_Net.Startup))]

namespace Area_Net
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login")
            });
            APIManager.StartManaging();
        }
    }
}