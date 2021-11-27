using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFTDemo.Models
{
    public class NftModel
    {
        public int NftID { get; set; }

        public string IpfsUrl { get; set; }

        public int CustomerID { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
