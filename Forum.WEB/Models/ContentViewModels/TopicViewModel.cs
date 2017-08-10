using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models.ContentViewModels
{
    public class TopicViewModel:AbstractViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
        public  UserViewModel User { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}