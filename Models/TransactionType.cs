using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
