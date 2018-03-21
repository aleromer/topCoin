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

            string datetime = DateTime.Today.ToLongTimeString();
            chain.AddBlock(new Block(1, datetime, "block1"));

            datetime = DateTime.Today.ToLongTimeString();
            chain.AddBlock(new Block(2, datetime, "block2"));

            Console.WriteLine(chain.ValidateChain());

            // data manipulation
            chain.chain[1].Data = "3";
            Console.WriteLine(chain.ValidateChain());

            Console.ReadKey();
        }
    }
}
