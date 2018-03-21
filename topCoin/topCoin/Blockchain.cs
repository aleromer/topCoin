using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace topCoin
{
    public class Blockchain
    {
        public List<Block> chain = new List<Block>();
        private int Difficulty { get; set; }

        public Blockchain()
        {
            //Genesis Block. TODO: Get data from config file
            string datetime = DateTime.Today.ToLongTimeString();
            chain.Add(new Block(0, datetime, ConfigurationManager.AppSettings["GenesisData"], "0"));
            this.Difficulty = 2;
        }

        public Block GetLatestBlock()
        {
            return chain.Last();    
        }

        public void AddBlock(Block newBlock)
        {
            newBlock.PreviousHash = chain.Last().Hash;
            //replace for mineBlock
            //newBlock.Hash = newBlock.CalculateHash();

            newBlock.MineBlock(this.Difficulty);
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
