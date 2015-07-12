using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

namespace ZonBarcode    
{
    [TestClass]
    public class UnitTest1
    {

        private string getRandomString(int length)
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, 55)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

        }
        [TestMethod]
        public void TestCrypto()
        {


            var plain = getRandomString(101);
            
            byte[] plainBytes = Encoding.UTF8.GetBytes(plain);

            var ciph = SecureLocalStore.protect(plain);

            var plain2 = Encoding.UTF8.GetString(SecureLocalStore.unprotect(ciph));

            Assert.AreEqual(plain, plain2);


            
        }

        
        [TestMethod]
        public void TestSecureStorage()
        {


            var plain = getRandomString(101);
            var name = getRandomString(10);

            SecureLocalStore.storeItem(name, plain);

            var plain2 = SecureLocalStore.getItem(name);


            Assert.AreEqual(plain, plain2);


            
        }

        [TestMethod]
        public void TestDataStore()
        {


            InventoryMember i = new InventoryMember("x", "y", "z");
            DataStore.Upsert_Inventory(i);

            InventoryMember i2 = DataStore.getInventoryByAsin(i.ASIN);

            Assert.IsNotNull(i2);
            Assert.AreEqual(i2.FNSKU, i.FNSKU);

            Product p = new Product("z", "a");
            Product p2 = DataStore.getProductByAsin(p.ASIN);

            
            
        }

        

    }
}
