using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int? NftId { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int? TransactionTypeId { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Group Group { get; set; }
        public virtual Nft Nft { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual User User { get; set; }
    }
}
