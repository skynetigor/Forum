using Microsoft.AspNet.Identity.EntityFramework;
using Forum.DAL.Entities.Identity.IntPk;
namespace Forum.DAL.Entities
{
    public class ApplicationRole : IdentityRole<int, UserRoleIntPk>
    {
        
    }
}
