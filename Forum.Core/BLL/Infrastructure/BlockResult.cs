using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Core.BLL.Infrastructure
{
    public class BlockResult
    {
        private IEnumerable<BlockType> blocktype;
        private string message;
        public BlockResult(IEnumerable<BlockType> blockTypes, string message)
        {
            this.blocktype = blockTypes;
            this.message = message;
        }

        public IEnumerable<BlockType> BlockType
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
