﻿using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;
using Forum.IoC.Interfaces;
using Forum.IoC.Service;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Attributes
{
    public class MyAllowAnonymousAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                IDependencyInstaller dependencyInstaller = new CastleInstaller(Assembly.GetExecutingAssembly());
                IBlockService service = dependencyInstaller.GetService<IBlockService>();
                BlockResult blockresult = service.GetUserStatusByUserId(httpContext.User.Identity.GetUserId<int>());
                if (blockresult != null)
                {
                    if (blockresult.BlockType.Contains(BlockType.Access))
                    {
                        httpContext.GetOwinContext().Authentication.SignOut();
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url = filterContext.HttpContext.Request.Url.ToString();
            filterContext.Result = new RedirectResult(url);
        }
    }
}