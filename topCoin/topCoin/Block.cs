using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace topCoin
{
    class Block
    {
        public int Index { get; set; }
        public string PreviousHash { get; set; }
        public string Timestamp { get; set; }
        public string Data { get; set; }
        public string Hash { get; set; }

        public Block(int index, string timestamp, string data, string previousHash = "")
        {
            this.Index = index;
            this.PreviousHash = previousHash;
            this.Timestamp = timestamp;
            this.Data = data;
            this.Hash = this.CalculateHash();
        }

        public string CalculateHash()
        {
            string data = Index.ToString() + PreviousHash + Timestamp + Data.ToString() + Hash;

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
