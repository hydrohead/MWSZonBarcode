using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonBarcode
{
    public class InventoryMember
    {

        public string ASIN { get; set; }
        public string FNSKU { get; set; }
        public string SellerSKU { get; set; }

        public InventoryMember(string asin, string fnsku,string sellersku)
        {
            ASIN = asin;
            FNSKU = fnsku;
            SellerSKU = sellersku;
        }

        
    }
}
