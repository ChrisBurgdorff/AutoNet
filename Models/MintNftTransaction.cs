using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFTDemo.Models
{
    public class MintNftTransaction
    {
        public MintNftTransaction()
        {
            /*if (WalletAddress.Substring(0, 2) == "0x")
            {
                WalletAddress = WalletAddress.Substring(2, WalletAddress.Length - 2);
            }*/
        }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string WalletAddress { get; set; }
        public int UserID { get; set; }
        public string IpfsUrl { get; set; }
    }
}
