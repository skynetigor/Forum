using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Models.ContentViewModels
{
    public class CategoryViewModel:AbstractViewModel
    {
        public SelectList Users { get; set; }
        public int ModeratorId { get; set; }
        public string ModeratorName { get; set; }
        [Required]
        [Display(Name= "Описание")]
        public string Description { get; set; }
    }
}