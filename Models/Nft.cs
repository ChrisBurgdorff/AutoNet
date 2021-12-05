﻿using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class Nft
    {
        public Nft()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int NftId { get; set; }
        public string IpfsUrl { get; set; }
        public int? Success { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDisabled { get; set; }
        public int? WalletId { get; set; }

        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
