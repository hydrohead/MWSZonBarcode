using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZonBarcode
{
    public class MWSWebRequest
    {

        public string url_called { get; set; }
        public DateTime logtimestamp { get; set; }

        public string response { get; set; }
        public int durationms { get; set; }
        public int StatusCode { get; set; }
        public long ContentLength { get; set; }


        private const string DESTINATION = "mws.amazonservices.com";


        public bool isServiceUp(out string stringMsg)
        {


            //GetServiceStatus 



            IDictionary<string, string> r1 = new Dictionary<string, String>();

            r1["Action"] = "ListMatchingProducts";
            r1["Query"] = "nomatch";
            r1["Version"] = "2011-10-01";


           
            string s = getResponse("https://mws.amazonservices.com/Products/2011-10-01", r1, true, false);

            if (!String.IsNullOrEmpty(s) && s.Contains("ListMatchingProductsResult"))
            {
                stringMsg = "Success";
                return true;
            }
            else
            {
                stringMsg = "Failed - " + this.response;
                return false;
            }
        }

        public MWSWebRequest()
        {


        }



        public string getResponse(string serviceURL, IDictionary<string, string> parms)
        {
            return getResponse(serviceURL, parms, true, true);
        }

        public string getResponse(string serviceURL, IDictionary<string, string> parms, bool useMarketPlaceId, bool retry)
        {


            AMZNHelper amznHelper = new AMZNHelper();

            parms["SellerId"] = SecureLocalStore.getItem("SellerId");
            if (useMarketPlaceId)
            {
                parms["MarketplaceId"] = SecureLocalStore.getItem("MarketplaceId");
            }

            amznHelper.AddRequiredParameters(parms, serviceURL);


            //var content2 = HttpUtility.UrlEncode(str, System.Text.Encoding.UTF8);
            var content2 = new FormUrlEncodedContent(parms);
            var requestUrl = serviceURL + "?" + content2.ReadAsStringAsync().Result;


            this.url_called = requestUrl;
            this.logtimestamp = DateTime.Now;

            var start = DateTime.Now;

            WebRequest request = HttpWebRequest.Create(requestUrl);


            HttpWebResponse webResponse = null;

            try
            {
                webResponse = (HttpWebResponse)request.GetResponse();
                this.StatusCode = (int)webResponse.StatusCode;
                this.ContentLength = webResponse.ContentLength;

            }
            catch (WebException e)
            {

                // wait a bit, retry
                Util.ServiceLog.Warn("first error running " + requestUrl, e);
                if (retry)
                {
                    Util.ServiceLog.Info("waiting 2 mins...");
                    System.Threading.Thread.Sleep(120000);

                    try
                    {
                        start = DateTime.Now;
                        request = HttpWebRequest.Create(requestUrl);
                        webResponse = (HttpWebResponse)request.GetResponse();
                        this.StatusCode = (int)webResponse.StatusCode;
                        this.ContentLength = webResponse.ContentLength;
                    }
                    catch (WebException e2)
                    {
                        Util.ServiceLog.Error("Second try error running " + requestUrl, e2);
                        if (e2.Status == WebExceptionStatus.ProtocolError)
                        {
                            var exResponse = (HttpWebResponse)e2.Response;
                            if (exResponse != null)
                            {
                                this.StatusCode = (int)exResponse.StatusCode;
                                this.response = new StreamReader(exResponse.GetResponseStream()).ReadToEnd();
                            }
                        }

                    }
                }
                else
                {

                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {
                        var exResponse = (HttpWebResponse)e.Response;
                        if (exResponse != null)
                        {
                            this.StatusCode = (int)exResponse.StatusCode;
                            this.response = new StreamReader(exResponse.GetResponseStream()).ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Util.ServiceLog.Error("Exception running " + requestUrl, e);
                return "";
            }

            this.durationms = (int)(DateTime.Now - start).TotalMilliseconds;

            if (webResponse == null)
                return "";

            MemoryStream ms = new MemoryStream();

            webResponse.GetResponseStream().CopyTo(ms);
            ms.Seek(0, 0);
            string rv = Encoding.UTF8.GetString(ms.ToArray());

            if (this.StatusCode == (int)HttpStatusCode.OK)
            {
                this.response = Util.Compress(rv);
            }

            


            return rv;




        }


        
    }
}
