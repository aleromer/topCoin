using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace topCoin
{
    public class Block
    {
        public string PreviousHash { get; set; }
        public string Timestamp { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; } // nonce value. This is a number that gets incremented until a good hash is found

        public Block(string timestamp, List<Transaction> pendingTransactions, string previousHash = "")
        {
            this.PreviousHash = previousHash;
            this.Timestamp = timestamp;
            this.Transactions = new List<Transaction>(pendingTransactions);
            this.Hash = this.CalculateHash();
            this.Nonce = 0;
        }

        public void MineBlock(int difficulty)
        {
            string difficultyZeros = "";
            difficultyZeros = difficultyZeros.PadLeft(difficulty, '0');
            while (this.Hash.Substring(0, difficulty) != difficultyZeros)
            {
                this.Nonce++;
                this.Hash = CalculateHash();
            }
            Console.WriteLine("Block mined! nonce:" + this.Nonce);
        }

        public string CalculateHash()
        {
            string data = this.PreviousHash + this.Timestamp + this.Transactions.ToString() + this.Nonce;

            byte[] bytes = Encoding.Unicode.GetBytes(data);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
        
    }
}
