using Forum.BLL.DTO.Content.Category;

namespace Forum.BLL.DTO.Content
{
    public class TopicDTO:BaseEntityDTO
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string UserName { get; set; }
        public int AnswersCount { get; set; }
    }
}
