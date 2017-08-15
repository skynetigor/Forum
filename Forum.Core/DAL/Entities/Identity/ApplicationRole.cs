using Microsoft.AspNet.Identity.EntityFramework;
using Forum.Core.DAL.Entities.Identity.IntPk;

namespace Forum.Core.DAL.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int, UserRoleIntPk>
    {
       
    }
}
