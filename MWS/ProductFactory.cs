using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZonBarcode
{
    public class ProductFactory
    {
        public ProductFactory()
        {

     
        }

        public Product getSingleProduct(string ASIN)
        {

            AMZNHelper amznHelper = new AMZNHelper();

            IDictionary<string, string> r1 = new Dictionary<string, String>();

            r1["Action"] = "GetMatchingProduct";
            r1["SellerId"] = "A3FWGQLMG1AAXG";
            r1["MarketplaceId"] = "ATVPDKIKX0DER";
            r1["ASINList.ASIN.1"] = ASIN;
            r1["Version"] = "2011-10-01";


            MWSWebRequest wr = new MWSWebRequest();
            string s = wr.getResponse("https://mws.amazonservices.com/Products/2011-10-01", r1, false, true);


            var xDoc = XDocument.Parse(s);


            XElement xe = Util.stripNS(xDoc.Elements().First());

            IEnumerable<XElement> Products = xe.Descendants("Product");
            foreach (XElement product in Products)
            {
                string title = Util.tryGetElementValueString(xe.Descendants("ItemAttributes").First(), "Title", true);

                Product p = new Product(ASIN, title);
                DataStore.Upsert_Product(p);
                return p;
            }

            return null;


        }

        public void loadProductsFromMWS()
        {


                //loop on inventory 
                foreach(InventoryMember im in DataStore.Inventory.Values)
                 {
                     Product p = getSingleProduct(im.ASIN);

                     var label = new printLabel();
                    
                     
                                  
                 }


            

        }
    }
}
