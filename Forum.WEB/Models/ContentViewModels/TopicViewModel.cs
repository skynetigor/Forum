using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models.ContentViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Текст сообщения")]
        public string Message { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}