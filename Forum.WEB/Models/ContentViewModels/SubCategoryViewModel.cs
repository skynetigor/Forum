using System.Web.Mvc;

namespace Forum.WEB.Models.ContentViewModels
{
    public class SubCategoryViewModel:CategoryViewModel
    {
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }
    }
}