using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.DTO.Content.Category
{
    public class SubCategoryDTO:AbstractCategoryDTO
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int TopicsCount { get; set; }
        public int AnswersCount { get; set; }
    }
}
