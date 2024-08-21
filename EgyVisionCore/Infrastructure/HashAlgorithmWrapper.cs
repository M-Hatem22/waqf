using System.Security.Cryptography;

namespace EgyVisionCore.Infrastructure
{
    public interface IHashAlgorithmWrapper
    {
        byte[] ComputeHash(byte[] buffer);
    }
    public class HashAlgorithmWrapper : IHashAlgorithmWrapper
    {
         private HashAlgorithm _HashAlgorithm = null;
         public  HashAlgorithmWrapper()
        {
            this._HashAlgorithm = SHA512.Create();
        }
        public virtual byte[] ComputeHash(byte[] buffer)
        {
            return _HashAlgorithm.ComputeHash(buffer);
        }
    }
}
