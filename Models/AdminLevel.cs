using System;
using System.Collections.Generic;

#nullable disable

namespace NFTDemo.Models
{
    public partial class AdminLevel
    {
        public AdminLevel()
        {
            Users = new HashSet<User>();
        }

        public int AdminLevelId { get; set; }
        public string AdminLevelName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
