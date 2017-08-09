using Forum.BLL.DTO.Content.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace Forum.WEB.Models.Category
{
    public class SubCategoryViewModel:CategoryViewModel
    {
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }
    }
}