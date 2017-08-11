using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Infrastructure
{
    public class BlockResult
    {
        private BlockType blocktype;
        private string message;
        public BlockResult(BlockType blockType, string message)
        {
            this.blocktype = blockType;
            this.message = message;
        }

        public BlockType BlockType
        {
            get
            {
                return blocktype;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }
}
