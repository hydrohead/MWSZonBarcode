using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ZonBarcode
{
    public static class Util
    {


        private static ILog _ServiceLog = null;

        internal static ILog ServiceLog {

            get
            {


                if (_ServiceLog == null)
                {

                    SetupLog();
                }
                return _ServiceLog;


            }
         }   
            
   

         public static void SetupLog()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = Application.UserAppDataPath + "\\ZonBarcodeLog.txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "10MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;            
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;

            _ServiceLog = LogManager.GetLogger("ServiceLog");

        }


       

        public static string tryGetElementValueString(XElement x, string name, bool warn)
        {
            if (x == null)
            {
                if (warn)
                {
                    ServiceLog.Warn("Problem getting element " + name + " from null element");

                }
                return "";
            }


            try
            {
                return x.Element(name).Value;

            }
            catch (Exception e)
            {
                if (warn)
                {
                    ServiceLog.Warn("Problem getting element " + name + " from " + x.ToString());
                }

                return "";
            }

        }

        public static Double? tryGetElementValueDouble(XElement x, string name, bool warn)
        {

            if (x == null)
            {
                if (warn)
                {
                    ServiceLog.Warn("Problem getting element " + name + " from null element");

                }
                return null;
            }
            try
            {
                string s = x.Element(name).Value;
                return Double.Parse(s);

            }
            catch (Exception e)
            {
                if (warn)
                    ServiceLog.Warn("Problem getting element " + name + " from " + x.ToString());

                return null;
            }

        }

        public static DateTime? tryGetElementValueDate(XElement x, string name, bool warn)
        {
            if (x == null)
            {
                if (warn)
                {
                    ServiceLog.Warn("Problem getting element " + name + " from null element");

                }
                return null;
            }
            try
            {
                string s = x.Element(name).Value;

                return DateTime.Parse(s);

            }
            catch (Exception e)
            {
                if (warn)
                    ServiceLog.Warn("Problem getting element " + name + " from " + x.ToString());

                return null;
            }

        }

        public static int? tryGetElementValueint(XElement x, string name, bool warn)
        {
            if (x == null)
            {
                if (warn)
                {
                    ServiceLog.Warn("Problem getting element " + name + " from null element");

                }
                return null;
            }
            try
            {
                string s = x.Element(name).Value;

                return int.Parse(s);

            }
            catch (Exception e)
            {
                if (warn)
                    ServiceLog.Warn("Problem getting element " + name + " from " + x.ToString());

                return null;
            }

        }


        public static XElement stripNS(XElement root)
        {
            return new XElement(
                root.Name.LocalName,
                root.HasElements ?
                    root.Elements().Select(el => stripNS(el)) :
                    (object)root.Value
            );
        }

        public static DataTable TsvToDT(string TabSepString)
        {


            IEnumerable<string> reader = TabSepString.Split('\n');


            var data = new DataTable();

            //this assume the first record is filled with the column names
            var headers = reader.First().Split('\t');
            foreach (var header in headers)
            {
                data.Columns.Add(header);
            }

            var records = reader.Skip(1);
            foreach (var record in records)
            {
                data.Rows.Add(record.Split('\t'));
            }

            return data;

        }


        public static string DeCompress(string CompressedString)
        {
            byte[] buffer = Convert.FromBase64String(CompressedString);

            using (var msi = new MemoryStream(buffer))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);

                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }



        }
        public static string Compress(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);


                }

                return Convert.ToBase64String(mso.ToArray());
            }


        }

        public static void MapXml(XElement xe, Object o)
        {


        }

        //public bool UIEditProperty(string propName, string val)
        //{

        //    try
        //    {

        //        //var decoded = HttpUtility.UrlDecode(val);


        //        PropertyInfo pi = typeof(ActionGroup).GetProperty(propName);


        //        if (pi != null)
        //        {
        //            var disallowUIEdit = Attribute.IsDefined(pi, typeof(DisallowUIEdit));

        //            if (!disallowUIEdit)
        //            {



        //                if(pi.PropertyType == typeof(int))
        //                {
        //                    int intval;

        //                    if(Int32.TryParse(val, out intval))
        //                    {
        //                        pi.SetValue(this, intval, null);
        //                        this.logActionGroup(true);
        //                        return true;
        //                    }
        //                }

        //                if (pi.PropertyType == typeof(string))
        //                {
        //                    pi.SetValue(this, val, null);
        //                        this.logActionGroup(true);
        //                        return true;

        //                }

        //                ActionLog.Warn("UIEditProperty got a type it did not know what to do with for " + propName);
        //                return false;



        //            }
        //            else
        //            {
        //                ActionLog.Warn("Tried to set a DisallowUIEdit property");
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            ActionLog.Warn("Did not get a property for " + propName);
        //            return false;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        ActionLog.Warn("Error setting property", e);
        //        return false;
        //    }



        



        

    }
}
