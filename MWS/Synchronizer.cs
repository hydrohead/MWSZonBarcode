using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonBarcode
{
    public class Synchronizer
    {

        private DateTime _LastSync = DateTime.MinValue;
        private const int maxAge = 30;
        private static object syncLock = new Object();


        public DateTime LastSync{

        get{ return _LastSync;}
    }
        public Synchronizer()
        {
            try
            {
                // this does not have to be encrypted but it is convenient.
                string s = SecureLocalStore.getItem("LastSync");
                if(!DateTime.TryParse(s,out _LastSync))
                {
                    _LastSync = DateTime.MinValue;
                }


                
            }
            catch(Exception e)
            {
                Util.ServiceLog.Error("Error getting last sync time");
                _LastSync = DateTime.MinValue;
            }
             
        }


        public void SyncData()
        {
            lock (syncLock)
            {
                _LastSync = DateTime.Now;
                InventoryFactory invF = new InventoryFactory();
                invF.getAllInventoryItems();
                ProductFactory pf = new ProductFactory();
                pf.loadProductsFromMWS();
                DataStore.createInitialBindingList();
                
            }

        }
        public void SyncLoop()
        {

            while (true)
            {
                try
                {

                    Double TotalMinutes = (DateTime.Now - _LastSync).TotalMinutes;
                    if (TotalMinutes > maxAge)
                    {
                       
                        SyncData();
                        System.Threading.Thread.Sleep(maxAge * 1000 * 60);
                    }
                    else
                    {
                        //sleep for remaining
                        System.Threading.Thread.Sleep((int)((maxAge - TotalMinutes) * 60000));
                    }
                
                }
                catch(Exception e)
                {
                    Util.ServiceLog.Error("Error in sync loop", e);
                    System.Threading.Thread.Sleep(5000);

                }
            }


        }
    }
}
