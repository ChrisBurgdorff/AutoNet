using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Nfts = new HashSet<Nft>();
        }

        public int WalletId { get; set; }
        public string WalletAddress { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Nft> Nfts { get; set; }
    }
}
