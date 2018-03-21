using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace topCoin.Tests
{
    [TestClass]
    public class BlockchainTests
    {
        [TestMethod]
        public void ValidateChainIntegrity()
        {
            Blockchain chain = new topCoin.Blockchain();

            string datetime = DateTime.Today.ToLongTimeString();
            chain.AddBlock(new Block(1, datetime, "block1"));

            datetime = DateTime.Today.ToLongTimeString();
            chain.AddBlock(new Block(2, datetime, "block2"));

            bool chainIsvalid = chain.ValidateChain();

            // data manipulation
            chain.chain[1].Data = "3";
            bool chainIsInvalid = chain.ValidateChain();

            Assert.AreEqual(false, chainIsvalid == chainIsInvalid);
        }
    }
}
