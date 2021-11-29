using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class User
    {
        public User()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Email { get; set; }
        public int? GroupId { get; set; }
        public int? AdminLevelId { get; set; }
        public DateTime? DateDisabled { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual AdminLevel AdminLevel { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
