using Forum.BLL.DTO.Content;

namespace Forum.BLL.DTO
{
    public class UserDTO:BaseEntityDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
