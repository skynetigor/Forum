using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Forum.NewBLL.Services
{
    public class BlockService : IBlockService
    {
        private IIdentityProvider identity;
        public BlockService(IIdentityProvider identity)
        {
            this.identity = identity;
        }

        public IEnumerable<BlockType> GetUserStatusByUserId(int id)
        {
            var appuser = identity.UserManager.FindById(id);
            if(!identity.UserManager.IsInRole(appuser.Id, "admin"))
            {
                    var list = new List<BlockType>();
                    if (appuser.IsAccessBlocked)
                        list.Add(BlockType.Access);
                    if (appuser.IsCommentBlocked)
                        list.Add(BlockType.Comment);
                    if (appuser.IsTopicBlocked)
                        list.Add(BlockType.Topic);
                    return list;
            }
            return null;
        }
    }
}
