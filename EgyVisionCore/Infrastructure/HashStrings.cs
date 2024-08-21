using System;
using System.Security.Cryptography;
using System.Text;

namespace EgyVisionCore.Infrastructure
{
    public interface IHashStrings
    {
        string getHash(string val);
        string getHash(string val, string key);
    }
    public class HashStrings : IHashStrings
    {
        private IHashAlgorithmWrapper _HashAlgorithm = null;
        public HashStrings(IHashAlgorithmWrapper HashAlgorithm)
        {
            this._HashAlgorithm = HashAlgorithm;
        }
        public virtual string getHash(string val)
        {
            byte[] btVal = System.Text.UnicodeEncoding.Unicode.GetBytes(val);

            byte[] btHashed = _HashAlgorithm.ComputeHash(btVal);

            string ret = System.Text.UnicodeEncoding.Unicode.GetString(btHashed);

            return ret;
        }

        public string getHash(string val, string key)
        {
            byte[] secretKeyBytes= Encoding.UTF8.GetBytes(key);
            byte[] data = Encoding.UTF8.GetBytes(val);
            using (HMACSHA256 hmac = new HMACSHA256(secretKeyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(data);
                string hashString = Convert.ToBase64String(hashBytes);

                return hashString;
            }
        }
    }
}
