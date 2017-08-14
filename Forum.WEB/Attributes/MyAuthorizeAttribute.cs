using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Forum.IoC.Interfaces;
using Forum.IoC.Service;
using System.Reflection;
using Forum.BLL.Interfaces;
using Forum.WEB.Controllers;
using Forum.BLL.Infrastructure;
using Microsoft.AspNet.Identity;
using System.Collections;
using Microsoft.Owin.Security;

namespace Forum.WEB.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private string message;
        protected bool accessError;
        public BlockType Permission { get; set; }

        protected bool CheckAccess(BlockType Permissions, HttpContextBase httpContext)
        {
            IDependencyInstaller dependencyInstaller = new CastleInstaller(Assembly.GetExecutingAssembly());
            IBlockService service = dependencyInstaller.GetService<IBlockService>();
            BlockResult blockresult = service.GetUserStatusByUserId(httpContext.User.Identity.GetUserId<int>());
            if (blockresult != null)
            {
                if (blockresult.BlockType.Contains(BlockType.Access))
                {
                    httpContext.GetOwinContext().Authentication.SignOut();
                }
                if (blockresult.BlockType.Contains(Permissions))
                {
                    message = blockresult.Message;
                    return false;
                }
            }
            return true;
        }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IIdentity identity = httpContext.User.Identity;
            if (identity.IsAuthenticated)
            {
               return CheckAccess(Permission, httpContext);
            }
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            ErrorController controller = new ErrorController();
            filterContext.Result = controller.GetError(message);
        }
    }
}