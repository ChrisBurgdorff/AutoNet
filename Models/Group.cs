using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class Group
    {
        public Group()
        {
            Transactions = new HashSet<Transaction>();
            Users = new HashSet<User>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDisabled { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
