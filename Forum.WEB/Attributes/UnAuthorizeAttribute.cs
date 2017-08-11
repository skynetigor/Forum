using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Attributes
{
    public class UnAuthorizeAttribute : MyAuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return true;
            else
            {
                return base.AuthorizeCore(httpContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Category/index");
        }
    }
}