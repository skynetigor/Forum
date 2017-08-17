using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Models.ContentViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Display(Name = "Текст сообщения")]
        public string Message { get; set; }
        
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}