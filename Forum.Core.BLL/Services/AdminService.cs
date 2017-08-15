using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Identity;
using Forum.Core.DAL.Interfaces;
using System;
using System.Linq;

namespace Forum.NewBLL.Services
{
    public class AdminService : IAdminService
    {
        const string ADMIN_ROLE = "admin";
        const string REASON = "Причина: ";
        const string BLOCK = "У вас ограниченен доступ к {0}. За информацией Обратитесь к администратору форума. ";
        const string UNBLOCK = "Для вас разблокирован доступ к {0}. ";
        const string COMMENT = "комментированию";
        const string TOPIC = "созданию топиков";
        const string RESOURCE = "ресурсу";
        private IIdentityProvider identity;
        private IGenericRepository<Block> blockRepository;
        public AdminService(IIdentityProvider identity, IGenericRepository<Block> blockRepository)
        {
            this.identity = identity;
            this.blockRepository = blockRepository;
        }

        private string GetBlockMessage(Block blockinfo)
        {
            string result = string.Empty;
            if (blockinfo.IsAccess)
            {
                result += RESOURCE;
            }
            else
            {
                if (blockinfo.IsComment)
                {
                    result += COMMENT + ",";
                }
                if (blockinfo.IsTopic)
                {
                    result += TOPIC;
                }
            }
            return string.Format(BLOCK, result);
        }

        private string GetUnBlockMessage(Block newblock, Block oldBlock)
        {
            string result = string.Empty;
            if (!newblock.IsAccess && oldBlock.IsAccess)
            {
                result += RESOURCE;
            }
            else
            {
                if (!newblock.IsComment && oldBlock.IsComment)
                {
                    result += COMMENT + ",";
                }
                if (!newblock.IsTopic && oldBlock.IsTopic)
                {
                    result += TOPIC;
                }
            }
            return string.Format(UNBLOCK, result, newblock.Reason);
        }

        public OperationDetails Block(Block blockinfo)
        {
            try
            {
                var appuser = identity.UserManager.Users.First(t => t.Id == blockinfo.User.Id);
                var block = appuser.Block;
                if (block != null && !blockinfo.IsTopic && !blockinfo.IsComment && !blockinfo.IsAccess)
                {
                    blockRepository.Remove(block);
                }
                else
                {
                    if (block == null)
                    {
                        block = new Block();
                    }
                    blockinfo.Message = GetBlockMessage(blockinfo);
                    blockinfo.User = appuser;
                    if (block.Id != 0)
                    {
                        blockRepository.Update(blockinfo);
                    }
                    else
                    {
                        blockRepository.Create(blockinfo);
                    }
                }
                return new OperationDetails(true, string.Empty);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message);
            }
        }
    }
}
