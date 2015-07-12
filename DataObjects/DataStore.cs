using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZonBarcode
{
    public static class DataStore
    {
        public static SortedList<string,InventoryMember> Inventory = new SortedList<string,InventoryMember>();
        public static SortedList<string, Product> Products = new SortedList<string, Product>();
        public static BindingList<FNSkuLabel> Labels = new BindingList<FNSkuLabel>();

        
        private static FileStream ProductsFile = new FileStream(Application.UserAppDataPath + "\\ZonBarcode_Products.txt", FileMode.OpenOrCreate);
        private static FileStream InventoryFile = new FileStream(Application.UserAppDataPath + "\\ZonBarcode_Inventory.txt", FileMode.OpenOrCreate);
        private static bool loadedFiles = false;
        

        public static void createInitialBindingList()
        {
            if(Labels==null)
            {
                Labels = new BindingList<FNSkuLabel>();

            }
            Labels.Clear();

            foreach(InventoryMember inv in Inventory.Values)

            {
                if (Products.ContainsKey(inv.ASIN))
                {
                    Product p = Products[inv.ASIN];
                    var label = new FNSkuLabel(inv.ASIN, inv.FNSKU, p.Title, "New", 1, 3);
                    Labels.Add(label);
                }
            }
            
        }
        
        public static InventoryMember getInventoryByAsin(string ASIN)
        {
            loadFiles();

            if(Inventory.ContainsKey(ASIN))
            {
                return Inventory[ASIN];
            }
            else
            {
                return null;
            }

        }

        public static Product getProductByAsin(string ASIN)
        {
            loadFiles();
            if (Products.ContainsKey(ASIN))
            {
                return Products[ASIN];
            }
            else
            {
                return new Product(ASIN, "");
            }


        }

        public static void Upsert_Product(Product p)
        {
            loadFiles();
            lock (Products)
            {
                Products[p.ASIN] = p;

                if (Inventory.ContainsKey(p.ASIN))
                {
                    InventoryMember inv = Inventory[p.ASIN];
                    var label = new FNSkuLabel(inv.ASIN, inv.FNSKU, p.Title, "New", 1, 3);
                    if (Labels.Contains(label))
                    {
                        Labels.Remove(label);
                    }
                    Labels.Add(label);
                }
            }
            saveProduct();
        }

        public static void Upsert_Inventory(InventoryMember inv)
        {
            loadFiles();

            lock (Inventory)
            {
                Inventory[inv.ASIN] = inv;

                if (Products.ContainsKey(inv.ASIN))
                {
                    Product p = Products[inv.ASIN];

                    var label = new FNSkuLabel(inv.ASIN, inv.FNSKU, p.Title, "New", 1, 3);
                    if (Labels.Contains(label))
                    {
                        Labels.Remove(label);
                    }
                    Labels.Add(label);
                }

            }
            saveInventory();
        }


        private static void saveProduct()
        {
            lock(Products)
            {

                var ProductString = JsonConvert.SerializeObject(Products);
                if(!ProductsFile.CanWrite)
                {
                   ProductsFile = new FileStream(Application.UserAppDataPath + "\\ZonBarcode_Products.txt", FileMode.OpenOrCreate);
                }
                ProductsFile.SetLength(0);
                using (StreamWriter sw = new StreamWriter(ProductsFile))
                {
                    sw.Write(ProductString);

                }

            }

        }

        private static void saveInventory()
        {

            
            lock(Inventory)
            {
                var invString = JsonConvert.SerializeObject(Inventory);

                if (!InventoryFile.CanWrite)
                {
                    InventoryFile = new FileStream(Application.UserAppDataPath + "\\ZonBarcode_Inventory.txt", FileMode.OpenOrCreate);
                }

                InventoryFile.SetLength(0);
                using (StreamWriter sw = new StreamWriter(InventoryFile))
                {
                    sw.Write(invString);

                }




            }

        }

        public static void loadFiles()
        {

            if(!loadedFiles)
            {

                loadInventory();
                loadProducts();
                loadedFiles = true;
                createInitialBindingList();

            }
        }
        private static void loadInventory()
        {


            InventoryFile.Seek(0, 0);

            using (StreamReader sr = new StreamReader(InventoryFile))
            {

                try
                {
                    Inventory = JsonConvert.DeserializeObject<SortedList<string, InventoryMember>>(sr.ReadToEnd());
                    
                }
                catch (Exception e)
                {
                    Util.ServiceLog.Error("Error getting Inventory from disk", e);
                }
                if (Inventory == null)
                {
                    Inventory = new SortedList<string, InventoryMember>();
                }

            }


        }

        private static void loadProducts()
        {

            ProductsFile.Seek(0, 0);
            
            using (StreamReader sr = new StreamReader(ProductsFile))
            {
               
                try
                {
                    Products = JsonConvert.DeserializeObject<SortedList<string, Product>>(sr.ReadToEnd());

                }
                catch(Exception e )
                {
                    Util.ServiceLog.Error("Error getting products from disk", e);
                }

                if (Products == null)
                {
                    Products = new SortedList<string, Product>();
                }
            }

        }


    }
}
