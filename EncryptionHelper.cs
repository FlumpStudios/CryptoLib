/*--------------------------------------------------------------------------------*
            CryptoLib Rijndael Encyptions Library - By Paul Marrable
            
          This libary is free to use but please leave this comment here :)
*---------------------------------------------------------------------------------*/


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib
{
    public class EncryptionHelper
    {
        /// <summary>
        /// Add encryption so string based on provided inputString and Salt
        /// </summary>
        /// <param name="text"></param>
        /// <param name="inputString"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncryptString(string text, string inputString,  string salt)
        {
            if (string.IsNullOrEmpty(text)) return text;                

            var aesAlg = NewRijndaelManaged(salt, inputString);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        /// <summary>
        /// Decrypt so string based on provided inputString and Salt
        /// </summary>
        /// <param name="text"></param>
        /// <param name="inputString"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string DecryptString(string encryptedText, string inputString, string salt)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return encryptedText;

            var aesAlg = NewRijndaelManaged(salt,inputString);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var cipher = Convert.FromBase64String(encryptedText);

            using (var msDecrypt = new MemoryStream(cipher))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Generate a new RijndaelManaged
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static RijndaelManaged NewRijndaelManaged(string salt, string inputString)
        {
            if (salt == null) throw new ArgumentNullException();
            var saltBytes = Encoding.ASCII.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(inputString, saltBytes);

            return new RijndaelManaged()
            {
                Key = key.GetBytes(32),
                IV = key.GetBytes(16)
            };
        }
    }
}
