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
        public List<Transaction> pendingTransactions = new List<Transaction>();
        private int Difficulty { get; set; }
        public int MiningReward { get; set; }
        public string MiningRewardFrom { get; set; }

        public Blockchain()
        {
            //Genesis Block. TODO: Get data from config file
            string datetime = DateTime.Today.ToLongTimeString();
            chain.Add(new Block(datetime, new List<Transaction>(), "0"));
            this.Difficulty = 2;
            this.MiningReward = Convert.ToInt32(ConfigurationManager.AppSettings["MiningReward"]);
            this.MiningRewardFrom = ConfigurationManager.AppSettings["MiningFrom"];
        }

        public Block GetLatestBlock()
        {
            return chain.Last();    
        }
        
        public void CreateTransaction(Transaction transaction)
        {
            //validate that the user from has enough funds or that the addresses exists

            pendingTransactions.Add(transaction);
        }

        public void MineBlock(string miningRewardAddress)
        {
            // Create new block with all pending transactions and mine it
            Block newBlock = new Block(DateTime.Today.ToLongTimeString(), pendingTransactions, this.chain.Last().Hash);
            //maybe this has to change to POA
            newBlock.MineBlock(this.Difficulty); //sets the nonce
            chain.Add(newBlock);// need to persist this to a file and distribute it to the nodes

            //clear the pending transaction list
            this.pendingTransactions.Clear();
            //send the mining reward. It will be added to the next block
            this.CreateTransaction(new Transaction(this.MiningRewardFrom, miningRewardAddress, this.MiningReward, null));
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

        public decimal CheckBalance(string address)
        {
            //para chequear el balance hace todas las cuentas de las transactions?
            decimal balance = 0;
            foreach (Block item in chain)
            {
                foreach (Transaction transaction in item.Transactions)
                {
                    if (transaction.FromAddress == address)
                    {
                        balance = balance - transaction.Amount;
                    }
                    if (transaction.ToAddress == address)
                    {
                        balance = balance + transaction.Amount;
                    }
                }
            }

            return balance;

        }
    }
}
