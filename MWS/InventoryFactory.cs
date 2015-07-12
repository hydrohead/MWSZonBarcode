using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZonBarcode
{
    public class InventoryFactory
    {
        public InventoryFactory()
        {

     
        }

        public InventoryMember getSingleInventoryItem(string ASIN)
        {
            //TODO
            return null;


        }

        public void getAllInventoryItems()
        {


            AMZNHelper amznHelper = new AMZNHelper();

            IDictionary<string, string> r1 = new Dictionary<string, String>();

            r1["Action"] = "ListInventorySupply";
            r1["SellerId"] = "A3FWGQLMG1AAXG";
            r1["MarketplaceId"] = "ATVPDKIKX0DER";
            r1["QueryStartDateTime"] = amznHelper.GetFormattedTimestamp(DateTime.Now.AddMonths(-6));
            r1["Version"] = "2010-10-01";


            MWSWebRequest wr = new MWSWebRequest();
            string s = wr.getResponse("https://mws.amazonservices.com/FulfillmentInventory/2010-10-01", r1, false, true);


            var xDoc = XDocument.Parse(s);


            XElement xe = Util.stripNS(xDoc.Elements().First());

            IEnumerable<XElement> InventoryMembers = xe.Descendants("member");
            foreach (XElement member in InventoryMembers)
            {
                string asin = Util.tryGetElementValueString(member, "ASIN", true);
                string SellerSKU = Util.tryGetElementValueString(member, "SellerSKU", true);
                string fnsku = Util.tryGetElementValueString(member, "FNSKU", true);

                InventoryMember i = new InventoryMember(asin, fnsku, SellerSKU);
                DataStore.Upsert_Inventory(i);
                
            }

            return;




        }
    }
}
