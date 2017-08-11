using Forum.BLL.Interfaces;
using Forum.IoC.Interfaces;
using Forum.IoC.Service;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Reflection;

[assembly: OwinStartup(typeof(Forum.WEB.App_Start.Startup))]

namespace Forum.WEB.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IAuthManager CreateUserService()
        {
            IDependencyInstaller dependencyInstaller = new CastleInstaller(Assembly.GetExecutingAssembly());
            return dependencyInstaller.Resolve<IAuthManager>();
        }
    }
}