using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Interfaces
{
    public interface ICategoryEditorService
    {
        void CreateCategory(UserDTO user, AbstractCategoryDTO category);
        void RemoveCategory(UserDTO user, AbstractCategoryDTO category);
        void UpdateCategory(UserDTO user, AbstractCategoryDTO category);
        void BlockCategory(UserDTO user, AbstractCategoryDTO category);
    }
}
