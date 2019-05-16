using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dosya_Sifreleme_Imzalama
{
    class Symmetric_Crypto
    {
        private static byte[] iv = new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public static byte[] generateKey() //Her oturum için AES anahtarı oluşturur
        {
            try
            {
                var aes = Aes.Create();
                aes.GenerateKey();
                return aes.Key;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static byte[] encrypt(byte[] plainText, byte[] key) //AES şifreleme işlemi yapar
        {
            byte[] cipherText;
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    // Create an encryptor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    cipherText = performCryptography(plainText, encryptor);

                }
                return cipherText;
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        public static byte[] decrypt(byte[] cipherText, byte[] key)//Aes deşifreleme işlemini yapar
        {
            byte[] plainText;
            try
            {
                var aes = Aes.Create();

                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                plainText = performCryptography(cipherText, decryptor);
              
                return plainText;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static byte[] performCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }
    }
}

