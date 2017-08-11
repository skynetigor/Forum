using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Attributes
{
    public class MyAuthorizeAttribute:AuthorizeAttribute
    {
        public MyAuthorizeAttribute():base()
        {

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext.User.Identity.IsAuthenticated)
            {
                bool s = !httpContext.User.IsInRole("blocked");
                return s;
            }
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if(filterContext.HttpContext.User.IsInRole("blocked"))
            {
                filterContext.Result = new RedirectResult("/BlockedUser/YouAreBlocked");
                return;
            }
            filterContext.Result = new RedirectResult("/Account/login");
            //filterContext.re
        }
    }
}