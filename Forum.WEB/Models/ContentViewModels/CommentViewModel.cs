using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models.ContentViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Message { get; set; }
    }
}