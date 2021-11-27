using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFTDemo.Models
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }

        public int NftID { get; set; }

        public int CustomerID { get; set; }

        public int UserID { get; set; }

        public int GroupID { get; set; }

        public int TransactionTypeID { get; set; }

        public bool Successful { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
