using Forum.WEB.Attributes;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class ErrorController : Controller
    {
        [MyAuthorize]
        public ActionResult GetError(string message)
        {
            if(!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
                return View("Message");
            }
            return View("Message");
        }
    }
}