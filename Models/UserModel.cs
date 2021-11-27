using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFTDemo.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        public string FullName { get; set; }

        public DateTime DateCreated { get; set; }

        public string Email { get; set; }

        public int GroupID { get; set; }

        public int AdminLevelID { get; set; }

        public DateTime Date_Disabled { get; set; }
    }
}
