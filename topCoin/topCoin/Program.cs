using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace topCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain chain = new topCoin.Blockchain();
            Console.WriteLine("Creating Transactions");
            chain.CreateTransaction(new Transaction("address1", "address2", 12, "codigo smart contract"));
            chain.CreateTransaction(new Transaction("address2", "address1", 4, "codigo smart contract"));
            chain.CreateTransaction(new Transaction("address3", "address2", 10, "codigo smart contract"));

            Console.WriteLine("mining block");
            chain.MineBlock("address3");

            Console.WriteLine("balance addr1: " + chain.CheckBalance("address1"));
            Console.WriteLine("balance addr2: " + chain.CheckBalance("address2"));
            Console.WriteLine("balance addr3: " + chain.CheckBalance("address3"));

            Console.WriteLine("mining block");
            chain.MineBlock("address3");
            Console.WriteLine("balance addr3: " + chain.CheckBalance("address3"));
            
            Console.WriteLine("Is chain valid?: " + chain.ValidateChain());

            Console.ReadKey();
        }
    }
}
