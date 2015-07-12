using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonBarcode
{
    public class Product
    {
        public string ASIN { get; set; }
        public string Title { get; set; }
        

        public Product(string asin, string title)
        {
            ASIN = asin;
            Title = title;
            

        }
    }


    
}
