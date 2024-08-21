using System;
using System.Text.RegularExpressions;

namespace EgyVisionCore.Infrastructure
{
    public interface IEncryptionHelper:IDisposable
    {
        string Encrypt(string toEncrypt);
        string Decrypt(string cipherString);
    }
    public class EncryptionHelper : IEncryptionHelper
    {
        public EncryptionHelper()
        {
        }

        public string Encrypt(string toEncrypt)
        {
            //0>> P,1>>o , 2>>z, 3>>c, 4>>K, 5>>n, 6>>m, 7>>E, 8>>s, 9>>f
            Regex rgx = new Regex("0");
            toEncrypt = rgx.Replace(toEncrypt, "P");
            rgx = new Regex("1");
            toEncrypt = rgx.Replace(toEncrypt, "o");
            rgx = new Regex("2");
            toEncrypt = rgx.Replace(toEncrypt, "z");
            rgx = new Regex("3");
            toEncrypt = rgx.Replace(toEncrypt, "c");
            rgx = new Regex("4");
            toEncrypt = rgx.Replace(toEncrypt, "K");
            rgx = new Regex("5");
            toEncrypt = rgx.Replace(toEncrypt, "n");
            rgx = new Regex("6");
            toEncrypt = rgx.Replace(toEncrypt, "m");
            rgx = new Regex("7");
            toEncrypt = rgx.Replace(toEncrypt, "E");
            rgx = new Regex("8");
            toEncrypt = rgx.Replace(toEncrypt, "s");
            rgx = new Regex("9");
            toEncrypt = rgx.Replace(toEncrypt, "f");

            return toEncrypt;
        }
        public string Decrypt(string cipherString)
        {
            //P>> 0,o>>1 , z>>2, c>>3, K>>4, 1>>5, m>>6, E>>7, s>>8, f>>9
            Regex rgx = new Regex("P");
            cipherString = rgx.Replace(cipherString, "0");
            rgx = new Regex("o");
            cipherString = rgx.Replace(cipherString, "1");
            rgx = new Regex("z");
            cipherString = rgx.Replace(cipherString, "2");
            rgx = new Regex("c");
            cipherString = rgx.Replace(cipherString, "3");
            rgx = new Regex("K");
            cipherString = rgx.Replace(cipherString, "4");
            rgx = new Regex("n");
            cipherString = rgx.Replace(cipherString, "5");
            rgx = new Regex("m");
            cipherString = rgx.Replace(cipherString, "6");
            rgx = new Regex("E");
            cipherString = rgx.Replace(cipherString, "7");
            rgx = new Regex("s");
            cipherString = rgx.Replace(cipherString, "8");
            rgx = new Regex("f");
            cipherString = rgx.Replace(cipherString, "9");

            return cipherString;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.Collect();
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
