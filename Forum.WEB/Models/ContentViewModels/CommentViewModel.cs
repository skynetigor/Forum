using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Models.ContentViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        [AllowHtml]
        public string Message { get; set; }
    }
}