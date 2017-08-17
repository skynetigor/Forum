using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Forum.IoC.Interfaces;
using Forum.IoC.Service;
using System.Reflection;
using Forum.WEB.Controllers;
using Microsoft.AspNet.Identity;
using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;

namespace Forum.WEB.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        const string ADMIN_ROLE = "admin";
        const string REASON = "Причина: ";
        const string BLOCK = "У вас ограниченен доступ к {0}. За информацией Обратитесь к администратору форума. ";
        const string UNBLOCK = "Для вас разблокирован доступ к {0}. ";
        const string COMMENT = "комментированию";
        const string TOPIC = "созданию топиков";
        const string RESOURCE = "ресурсу";

        private string message;
        public BlockType Permission { get; set; }

        private string GetBlockMessage(BlockType blockinfo)
        {
            string result = string.Empty;
            if (blockinfo == BlockType.Access)
            {
                result += RESOURCE;
            }
            else
            {
                if (blockinfo == BlockType.Comment)
                {
                    result += COMMENT + ",";
                }
                if (blockinfo == BlockType.Topic)
                {
                    result += TOPIC;
                }
            }
            return string.Format(BLOCK, result);
        }

        protected bool CheckAccess(BlockType Permissions, HttpContextBase httpContext)
        {
            var dependencyInstaller = new CastleInstaller(Assembly.GetExecutingAssembly());
            var service = dependencyInstaller.GetService<IBlockService>();
            var blockresult = service.GetUserStatusByUserId(httpContext.User.Identity.GetUserId<int>());
            if (blockresult != null)
            {
                if (blockresult.Contains(BlockType.Access))
                {
                    httpContext.GetOwinContext().Authentication.SignOut();
                }
                if (blockresult.Contains(Permissions))
                {
                    message = GetBlockMessage(Permission);
                    return false;
                }
            }
            return true;
        }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var identity = httpContext.User.Identity;
            if (identity.IsAuthenticated)
            {
               return CheckAccess(Permission, httpContext);
            }
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var controller = new ErrorController();
            filterContext.Result = controller.GetError(message);
        }
    }
}