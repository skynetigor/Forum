using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class TopicController : Controller
    {
        // GET: Topics
        public ActionResult Index()
        {
            return View();
        }
    }
}