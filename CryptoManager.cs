/*--------------------------------------------------------------------------------*
            CryptoLib Rijndael Encyptions Library - By Paul Marrable
            
          This libary is free to use but please leave this comment here :)
*---------------------------------------------------------------------------------*/

using System;
namespace CryptoLib
{
    public class CryptoManager : ICryptoManager
    {
        private readonly string _inputString;
        private readonly string _salt;

        public CryptoManager(string inputString, string salt)
        {
            _inputString = inputString;
            _salt = salt;
        }


        /// <summary>
        /// Encrypt or decrypt any element in passed model that is marked with an [Encrypt] sttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="encryptionOption"></param>
        /// <returns></returns>
        public T RunCipher<T>(T model, EncryptionOptions encryptionOption)
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                bool hasEncyptAttribute = Attribute.IsDefined(prop, typeof(EncryptAttribute));
                if (hasEncyptAttribute)
                {
                    if (prop.PropertyType !=  typeof(string))
                        throw new NotSupportedException("Value must be a string. Please ensure Encrypt attibutes are only added to properies of type string");

                    string val = prop.GetValue(model) as string;
                     
                    prop.SetValue(model, encryptionOption == EncryptionOptions.Encrypt ? GetEncryptedValue(val) : GetDecryptedValue(val));
                }
            }
            return model;
        }

        /// <summary>
        /// Get encrypted value from the Encyption helper
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetEncryptedValue(string value)
        {
            return EncryptionHelper.EncryptString(value, _inputString, _salt);
        }


        /// <summary>
        /// Get decyption value from the Encyption helper
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetDecryptedValue(string value)
        {
            return EncryptionHelper.DecryptString(value, _inputString, _salt);
        }
    }
 
}
