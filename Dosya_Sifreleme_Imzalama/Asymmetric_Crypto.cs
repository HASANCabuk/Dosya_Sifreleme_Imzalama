using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dosya_Sifreleme_Imzalama
{
    class Asymmetric_Crypto
    {
       public static void generateKeys(string publicKeyFile, string privateKeyFile)// public ve private keyleri yaratır
        {
            using (var rsa = new RSACryptoServiceProvider((int)2048))
            {
                rsa.PersistKeyInCsp = false;

                if (File.Exists(privateKeyFile))
                    File.Delete(privateKeyFile);

                if (File.Exists(publicKeyFile))
                    File.Delete(publicKeyFile);

                string publicKey = rsa.ToXmlString(false);
                File.WriteAllText(publicKeyFile, publicKey);
                string privateKey = rsa.ToXmlString(true);
                File.WriteAllText(privateKeyFile, privateKey);
            }
        }
        public static byte[] encrypt(byte[] plain, string publicKeyFile)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider((int)2048))
            {
                rsa.PersistKeyInCsp = false;
                string publicKey = File.ReadAllText(publicKeyFile);
                rsa.FromXmlString(publicKey);
                encrypted = rsa.Encrypt(plain, true);
            }
            return encrypted;
        }
        public static byte[] decrypt(byte[] encrypted, string privateKeyFile)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider((int)2048))
            {
                rsa.PersistKeyInCsp = false;
                string privateKey = File.ReadAllText(privateKeyFile);
                rsa.FromXmlString(privateKey);
                decrypted = rsa.Decrypt(encrypted, true);
            }

            return decrypted;
        }

        public static byte[] signData(byte[] orginalMessage, string privateKeyFile)// gelen dosyayı sha512 ile hash yapıp kullanıcının private anhatarı ile imzalar
        {
           
            byte[] signedBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {              
               
                try
                {
                    string privateKey = File.ReadAllText(privateKeyFile);
                    rsa.FromXmlString(privateKey);
                    signedBytes = rsa.SignData(orginalMessage, CryptoConfig.MapNameToOID("SHA512"));
                }
                catch (CryptographicException )
                {
                    throw;
                }
                finally
                {                  
                    rsa.PersistKeyInCsp = false;
                }
            }
            return signedBytes;
        }
        public static bool verifyData( byte[] originalMessage, byte[] signedMessage, string publicKeyFile)// Orginal metinin sha512 ile hashini alıp  imzalı metinin imzalayan kullanıcının public anahtarı ile açıp karşılaştırır
        {
            bool success = false;
            using (var rsa = new RSACryptoServiceProvider())
            {               
                try
                {
                    string publicKey = File.ReadAllText(publicKeyFile);
                    rsa.FromXmlString(publicKey);
                    success = rsa.VerifyData(originalMessage, CryptoConfig.MapNameToOID("SHA512"), signedMessage);                  
                }
                catch (CryptographicException )
                {
                    throw;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return success;
        }
    }
}






