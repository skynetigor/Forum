using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class TopicsController : Controller
    {
        public ActionResult Index(int Id)
        {
            return View();
        }
    }
}