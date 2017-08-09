using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Attributes
{
    public class UnAuthorizeAttribute : AuthorizeAttribute
    {
        public UnAuthorizeAttribute()
        {

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return !httpContext.User.Identity.IsAuthenticated;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Category/index");
        }
    }
}