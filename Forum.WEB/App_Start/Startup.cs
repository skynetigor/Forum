using Castle.Windsor;
using Forum.BLL.Interfaces;
using Forum.WEB.Util;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Forum.WEB.App_Start.Startup))]

namespace Forum.WEB.App_Start
{
    public class Startup
    {
        public Startup()
        {
            // создаем контейнер
            container = new WindsorContainer();
            // регистрируем компоненты с помощью объекта ApplicationCastleInstaller
            container.Install(new ApplicationCastleInstaller());
        }
        WindsorContainer container;
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            IUserService service = container.Resolve<IUserService>();
            return service;
        }
    }
}