using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forum.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "paging",
                url: "{controller}/{id}/page{page}",
                defaults: new { controller = "{controller}", action = "index", id = UrlParameter.Optional, page = 1 }
                );

            routes.MapRoute(
                            name: "Default",
                            url: "{controller}/{action}",
                            defaults: new { controller = "Account", action = "Login" }
                        );
        }
    }
}
