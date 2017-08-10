using Forum.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class NotificationController : Controller
    {
        private INotificationService notificationService;
        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost]
        public ActionResult GetNotification()
        {
            var userid = User.Identity.GetUserId<int>();
            var notifications = notificationService.GetNotificationsByUserId(userid);
            return Json(userid);
        }
    }
}