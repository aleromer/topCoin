using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace topCoin
{
    class Blockchain
    {
        List<Block> chain = new List<Block>();

        public Blockchain()
        {
            //Genesis Block. TODO: Get data from config file
            chain.Add(new Block(0, DateTime.Today.ToLongTimeString(), null, ""));
        }

        public Block GetLatestBlock()
        {
            return chain.Last();
        }

        public void AddBlock(Block newBlock)
        {
            newBlock.PreviousHash = chain.Last().PreviousHash;
            newBlock.Hash = newBlock.CalculateHash();
            chain.Add(newBlock);
        }

        public bool ValidateChain()
        {
            for (int i = 1; i < chain.Count; i++)
            {
                Block currentBlock = chain[i];
                Block previousBlock = chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.CalculateHash())
                {
                    return false;
                }

            }
            return true;
        }
    }
}
