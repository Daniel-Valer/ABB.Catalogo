using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.Utiles.Helpers
{
    public class EncriptacionHelper
    {
        //para BD SQL Server
        public static byte[] EncriptarByte(string rawText)
        {
            var rijndaelCipher = new RijndaelManaged();
            byte[] rawTextData = Encoding.UTF8.GetBytes(rawText);

            Rfc2898DeriveBytes secretKey = GetSecretKey();


            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(rawTextData, 0, rawTextData.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private static Rfc2898DeriveBytes GetSecretKey()
        {
            const string encryptionKey = "T@ll3rN3t2018";
            byte[] salt = Encoding.UTF8.GetBytes(encryptionKey);

            var secretKey = new Rfc2898DeriveBytes(encryptionKey, salt);
            return secretKey;
        }
        // para BD MySQL
        public static string GetMd5Hash(string key)
        {
            MD5 md5Hasher = MD5.Create();
            Byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(key));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string GetMd5Hash2(string key)
        {
            MD5 md5Hasher = MD5.Create();
            Byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(key));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
