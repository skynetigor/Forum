using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.DTO.Content.Category
{
    public class CategoryDTO:AbstractCategoryDTO
    {
        public CategoryDTO()
        {
            SubCategories = new List<SubCategoryDTO>();
        }
        public string ModeratorName { get; set; }
        public int ModeratorId { get; set; }
        public bool IsEdit { get; set; }
        public IEnumerable<SubCategoryDTO> SubCategories { get; set; }
    }
}
