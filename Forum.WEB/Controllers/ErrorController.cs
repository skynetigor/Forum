using Forum.WEB.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult GetError(string message)
        {
            if(!string.IsNullOrEmpty(message))
            {
                return View("Error",(object)message);
            }
            return View("Error");
        }
    }
}