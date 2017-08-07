using Castle.Windsor;
using Forum.WEB.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forum.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // создаем контейнер
            var container = new WindsorContainer();
            // регистрируем компоненты с помощью объекта ApplicationCastleInstaller
            container.Install(new ApplicationCastleInstaller());

            // Вызываем свою фабрику контроллеров
            var castleControllerFactory = new CastleControllerFactory(container);

            // Добавляем фабрику контроллеров для обработки запросов
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
