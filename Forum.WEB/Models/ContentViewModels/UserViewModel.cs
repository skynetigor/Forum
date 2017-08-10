using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models.ContentViewModels
{
    public class UserViewModel:AbstractViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; } 
    }
}