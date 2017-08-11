using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Services
{
    public class BlockService : IBlockService
    {
        const string COMMENT_ACCES_BLOCK = "Администратор ограничил для вас возможность комментирования. Причина: ";
        const string RESOURCE_ACCES_BLOCK = "Администратор ограничил для вас доступ к ресурсу. Причина: ";
        const string COMMENT_AND_TOPIC_BLOCK = "Администратор ограничил для вас возможность комментирования и создания новых тем. Причина: ";
        const string TOPIC_BLOCK = "Администратор ограничил для вас возможность комментирования и создания новых тем. Причина: ";

        private IUnitOfWork identity;
        public BlockService(IUnitOfWork identity)
        {
            this.identity = identity;
        }

        public BlockDTO GetUserBlockByUserId(int id)
        {
            var appuser = identity.UserManager.FindById(id);
            var appblock = appuser.Block;
            if (appblock == null)
            {
                return new BlockDTO
                {
                    UserId = id,
                    UserName = appuser.UserName
                };
            }
            return new BlockDTO
            {
                UserName = appuser.UserName,
                Message = appblock.Message,
                IsAccess = appblock.IsAccesBlock,
                IsComment = appblock.IsCommentBlock,
                IsTopic = appblock.IsTopicBlock,
                Reason = appblock.Reason,
                UserId = id
            };
        }

        public BlockDTO GetUserStatusByUserId(int id)
        {
            var appuser = identity.UserManager.FindById(id);
            if(!identity.UserManager.IsInRole(appuser.Id, "admin"))
            {
                var block = appuser.Block;
                if(block != null)
                {
                    return new BlockDTO
                    {
                        IsTopic = block.IsTopicBlock,
                        IsComment = block.IsCommentBlock,
                        IsAccess = block.IsAccesBlock,
                        UserId = appuser.Id,
                        Message = block.Message,
                        Reason = block.Message,
                        UserName = appuser.UserName
                    };
                }
            }
            return new BlockDTO();
        }
    }
}
