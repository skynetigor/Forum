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

        public Block GetUserBlockByUserId(int id)
        {
            Block block = null;
            var appuser = identity.UserManager.Users.FirstOrDefault(user => user.Id == id);
            if(appuser != null && !identity.UserManager.IsInRole(appuser.Id, "admin"))
            {
                block = appuser.Block;
            }
            if(block == null)
            { 
                block = new Block
                {
                    User = appuser
                };
            }
            return block;
        }

        public BlockResult GetUserStatusByUserId(int id)
        {
            var appuser = identity.UserManager.FindById(id);
            if(!identity.UserManager.IsInRole(appuser.Id, "admin"))
            {
                var block = appuser.Block;
                if(block != null)
                {
                    var list = new List<BlockType>();
                    if (block.IsAccess)
                        list.Add(BlockType.Access);
                    if (block.IsComment)
                        list.Add(BlockType.Comment);
                    if (block.IsTopic)
                        list.Add(BlockType.Topic);
                    return new BlockResult(list, block.Message);
                }
            }
            return null;
        }
    }
}
