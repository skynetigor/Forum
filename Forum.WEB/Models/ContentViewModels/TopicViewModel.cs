using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models.ContentViewModels
{
    public class TopicViewModel:AbstractViewModel
    {
        [Display(Name = "Текст сообщения")]
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}