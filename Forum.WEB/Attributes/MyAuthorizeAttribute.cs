using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Attributes
{
    public class MyAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext.User.Identity.IsAuthenticated)
            {
                bool s = !httpContext.User.IsInRole("blocked");
                return s;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if(filterContext.HttpContext.User.IsInRole("blocked"))
            {
                filterContext.Result = new RedirectResult("/BlockedUser/YouAreBlocked");
                return;
            }
            filterContext.Result = new RedirectResult("/Account/login");
        }
    }
}