using System.Collections.Generic;
using System.Security.Cryptography;

namespace Antauri.Transactions
{
    public class SignatureService
    {

    }
    public class Address
    {
        public string Value { get; set; }
    }
    public class SimpleTransaction
    {
        public Address Sender { get; set; }
        public Address Reciever { get; set; }
        public long Amount { get; set; }
    }
    public class SimpleTransactionBuilder
    {
        private SimpleTransaction currentTransaction;

        public SimpleTransactionBuilder()
        {
            currentTransaction = new SimpleTransaction();
        }

        public SimpleTransactionBuilder From(Address sender)
        {
            currentTransaction.Sender = sender;
            return this;
        }
        public SimpleTransactionBuilder SendTo(Address reciever, long Amount)
        {
            currentTransaction.Reciever = reciever;
            return this;
        }
        public SimpleTransaction Build()
        {
            var result =  currentTransaction;
            currentTransaction = null;
            return result;
        }
    }
    public class SimpleTransactionList: List<SimpleTransaction>
    {
        public byte[] GetTransactionBytes()
        {
            List<byte> bytes = new List<byte>();
            foreach(var transaction in this)
            {
                var str = transaction.Sender.Value + transaction.Amount + transaction.Reciever.Value;
                var strBytes = System.Text.Encoding.UTF8.GetBytes(str);
                bytes.AddRange(strBytes);
            }
            return bytes.ToArray();
        }
    }
}
