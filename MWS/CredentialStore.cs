using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Security.Cryptography.ProtectedData;


namespace ZonBarcode
{


    public static class SecureLocalStore
    {

        private static string ToHexString(string input)
        {
            return string.Join("", input.ToCharArray()
                .Select(c => string.Format("{0:x}", (int)c))
                .ToArray());
        }

        public static string getItem(string name)
        {

            try
            {
                string fname = Application.UserAppDataPath + "\\ZonBarcode_" + ToHexString(name) + ".txt";


                if(!File.Exists(fname))
                {
                    return "";
                }


                try
                {
                    byte[] cipherBytes = File.ReadAllBytes(fname);

                    var clearText = unprotect(cipherBytes);

                    return Encoding.UTF8.GetString(clearText);
                }
                catch(Exception e)
                {
                    //TODO
                    return "";
                }

                
            }
            catch (IOException e)
            {
                //TODO
                throw e;
            }
        }

        public static void storeItem(string name, string cleartextvalue)

        {

            try
            {

                string fname = Application.UserAppDataPath + "\\ZonBarcode_" + ToHexString(name) + ".txt";
                if(File.Exists(fname))
                {
                    File.Delete(fname);
                }

                // Create a file that the application will store user specific data in.
                using (var userData = new FileStream(fname, FileMode.OpenOrCreate))
                {

                    userData.Seek(0, 0);
                    var cipherText = protect(cleartextvalue);
                  //  byte[] cipherBytes = Encoding.UTF8.GetBytes(cipherText);
                    userData.Write(cipherText, 0, cipherText.Length);
                    userData.Flush();
                    userData.Close();
                 
                }

            }
            catch (IOException e)
            {
                //TODO
                throw e;
            }

        }

        private static byte[] getEntropy()
        {
            byte[] entropy = new byte[20];

            using (var entropyFile = new FileStream(Application.UserAppDataPath + "\\ZonBarcode_ent2.txt", FileMode.OpenOrCreate))
            {
                if (entropyFile.Length <= 0)
                {
                    createEntropy(entropyFile);
                }
             
                entropyFile.Read(entropy, 0, 20);
                entropyFile.Close();
            }
                 return entropy;
            


        }
        private static void createEntropy(FileStream entropyFile)
        {
            byte[] entropy = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }

            //string base64Entropy = Convert.ToBase64String(entropy);

            entropyFile.Write(entropy,0,20);
            entropyFile.Flush();
            entropyFile.Seek(0, 0);



        }

        
        public static byte[] protect(string cleartextvalue)
        {
            // Data to protect. Convert a string to a byte[] using Encoding.UTF8.GetBytes().
            byte[] plaintext = Encoding.UTF8.GetBytes(cleartextvalue.ToCharArray());


            
            byte[] ciphertext = ProtectedData.Protect(plaintext, getEntropy(),
                DataProtectionScope.CurrentUser);

            return ciphertext;

        }

        public static byte[] unprotect(byte[] cipherBytes)
        {

           // byte[] cipherBytes = Encoding.UTF8.GetBytes(ciphertext);

            byte[] plaintext = ProtectedData.Unprotect(cipherBytes, getEntropy(),
    DataProtectionScope.CurrentUser);

            return plaintext;

        }


    }
}
