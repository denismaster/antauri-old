using Antauri.Transactions;
using System;
using System.Text;

namespace Antauri.Core
{
    public class SimpleBlockFactory : IBlockFactory<SimpleBlock,SimpleTransactionList>, IGenesisBlockFactory<SimpleBlock>
    {
        private readonly IHashProvider _hasher;

        public SimpleBlockFactory(IHashProvider hasher)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public SimpleBlock CreateBlock(SimpleBlock lastBlock, SimpleTransactionList data)
        {
            long nextIndex = lastBlock.Header.Index + 1;
            long nextTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            var block = new SimpleBlock(nextIndex, lastBlock.Hash, nextTimestamp, data);

            _hasher.Hash(block);

            return block;
        }

        public SimpleBlock CreateGenesisBlock()
        {
            var transactions = new SimpleTransactionList
            {
                new SimpleTransaction()
                {
                    Sender = new Address(){ Value = "Satoshi"},
                    Reciever = new Address(){ Value = "denismaster"},
                    Amount = 25
                }
            };
            var block =  new SimpleBlock(0, "0", 1465154705, transactions );
            _hasher.Hash(block);
            return block;
        }
    }
}
