using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories();

        void CreateCategory(UserDTO currentUser, int subCategoryId);
        void RemoveCategory(UserDTO currentUser, int subCategoryId);
        void UpdateCategory(UserDTO currentUser, int subCategoryId);
        void BlockCategory(UserDTO currentUser, int subCategoryId);
    }
}
