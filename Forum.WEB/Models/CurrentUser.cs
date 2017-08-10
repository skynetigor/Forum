using Forum.BLL.DTO;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Forum.WEB.Models
{
    public static class CurrentUser
    {
        private static UserDTO currentUser;
        public static UserDTO Get(IPrincipal principal)
        {
            if(currentUser.Id != principal.Identity.GetUserId<int>())
            {
                UserDTO user = new UserDTO
                {
                    Email = principal.Identity.Name,
                    Id = principal.Identity.GetUserId<int>()
                };
                currentUser = user;
            }
            return currentUser;
        }
    }
}