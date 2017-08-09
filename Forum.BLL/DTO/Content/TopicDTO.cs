using Forum.BLL.DTO.Content.Category;

namespace Forum.BLL.DTO.Content
{
    public class TopicDTO
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
        public SubCategoryDTO SubCategory { get; set; }
    }
}
