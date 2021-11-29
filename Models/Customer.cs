using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Nfts = new HashSet<Nft>();
            Transactions = new HashSet<Transaction>();
            Wallets = new HashSet<Wallet>();
        }

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDisabled { get; set; }

        public virtual ICollection<Nft> Nfts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
