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
        ICollection<SubCategoryDTO> SubCategories { get; set; }
    }
}
