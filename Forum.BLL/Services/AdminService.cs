using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.BLL.Services
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
        private IUnitOfWork identity;
        private INotificationService notifyService;
        private IGenericRepository<Block> blockRepository;
        public AdminService(IUnitOfWork identity, INotificationService notifyService, IGenericRepository<Block> blockRepository)
        {
            this.identity = identity;
            this.notifyService = notifyService;
            this.blockRepository = blockRepository;
        }

        private string GetBlockMessage(BlockDTO blockinfo)
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

        private string GetUnBlockMessage(BlockDTO newblock, Block oldBlock)
        {
            string result = string.Empty;
            if (!newblock.IsAccess && oldBlock.IsAccess)
            {
                result += RESOURCE;
            }
            else
            {
                if (!newblock.IsComment && oldBlock.IsCommentBlock)
                {
                    result += COMMENT + ",";
                }
                if (!newblock.IsTopic && oldBlock.IsTopicBlock)
                {
                    result += TOPIC;
                }
            }
            return string.Format(UNBLOCK, result, newblock.Reason);
        }

        public OperationDetails Block(BlockDTO blockinfo)
        {
            try
            {
                var appuser = identity.UserManager.Users.First(t => t.Id == blockinfo.UserId);
                var block = appuser.Block;
                if (block != null && !blockinfo.IsTopic && !blockinfo.IsComment && !blockinfo.IsAccess)
                {

                    blockRepository.Remove(block);
                    notifyService.Notify(new UserDTO { Id = blockinfo.UserId, Email = appuser.Email }, GetUnBlockMessage(blockinfo, block));
                }
                else
                {
                    if (block == null)
                    {
                        block = new Block();
                    }
                    block.Message = GetBlockMessage(blockinfo);
                    block.Reason = blockinfo.Reason;
                    block.IsAccess = blockinfo.IsAccess;
                    block.IsCommentBlock = blockinfo.IsComment;
                    block.IsTopicBlock = blockinfo.IsTopic;
                    block.User = appuser;
                    if (block.Id != 0)
                    {
                        blockRepository.Update(block);
                    }
                    else
                    {
                        blockRepository.Create(block);
                    }
                    notifyService.Notify(new UserDTO { Id = blockinfo.UserId }, block.Reason);
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
