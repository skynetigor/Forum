using Forum.WEB.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class BlockedUserController : Controller
    {
        public ActionResult YouAreBlocked()
        {
            if (!User.IsInRole("blocked"))
                return RedirectToAction("Category","Index");
            return Json("Вы были заблокированы на данном ресурсе!", JsonRequestBehavior.AllowGet);
        }
    }
}