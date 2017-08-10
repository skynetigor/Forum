using Forum.WEB.Models.ContentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public interface IContentController<TViewModel> 
    {
        ActionResult Update(int? id);

        [HttpPost]
        ActionResult Update(TViewModel vieModel);
    }
}