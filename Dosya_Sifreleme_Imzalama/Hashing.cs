using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dosya_Sifreleme_Imzalama
{
    class Hashing
    {
        public static byte[] getSha512Hash(byte[] orginalData)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] computedHash = sha512.ComputeHash(orginalData);
            return computedHash;
        }
        public static bool compareHash(byte[] hash1, byte[] hash2)
        {

            string a = BitConverter.ToString(hash1);
            string b = BitConverter.ToString(hash2);

            return a == b;
        }
    }
}
