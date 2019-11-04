/*--------------------------------------------------------------------------------*
            CryptoLib Rijndael Encyptions Library - By Paul Marrable
            
          This libary is free to use but please leave this comment here :)
*---------------------------------------------------------------------------------*/
using System.Collections.Generic;
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
        public T RunCipher<T>(T model, EncryptionOptions encryptionOption)
        {
            if (model == null) return model;
            foreach (var prop in model.GetType().GetProperties())
            {
                bool hasEncyptAttribute = Attribute.IsDefined(prop, typeof(EncryptAttribute));
                bool hasEncyptClassAttribute = Attribute.IsDefined(prop, typeof(EncryptClassAttribute));
                bool hasEncyptCollectionAttribute = Attribute.IsDefined(prop, typeof(EncryptCollectionAttribute));

                if (hasEncyptAttribute)
                {
                    if (prop.PropertyType !=  typeof(string))
                        throw new NotSupportedException("Value must be a string. Please ensure Encrypt attibutes are only added to properies of type string");

                    string val = prop.GetValue(model) as string;
                    prop.SetValue(model, encryptionOption == EncryptionOptions.Encrypt ? GetEncryptedValue(val) : GetDecryptedValue(val));
                }

                //Run the Cipher recursivly if there is an 'Encrypt Class Attribute' on the prop                
                if (hasEncyptClassAttribute)
                    RunCipher(prop.GetValue(model, null), encryptionOption);

                /*If the 'Encrypt Collect Attribute' is present, recursivly loop through this function and 
                look for props with the 'Encrypt' Attribute */
                if (hasEncyptCollectionAttribute)
                {
                    foreach (var item in prop.GetValue(model, null) as IEnumerable<dynamic>)
                        RunCipher(item, encryptionOption);
                }
            }
            return model;
        }
        
        /// <summary>
        /// Run the Cipher on a collection of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList"></param>
        /// <param name="encryptionOption"></param>
        public IEnumerable<T> RunCipher<T>(IEnumerable<T> modelList, EncryptionOptions encryptionOption)
        {
            foreach (var model in modelList)
                yield return RunCipher(model, encryptionOption);
        }

        /// <summary>
        /// Get encrypted value from the Encyption helper
        /// </summary>
        /// <param name="value"></param>
        public string GetEncryptedValue(string value) =>
            EncryptionHelper.EncryptString(value, _inputString, _salt);

        /// <summary>
        /// Get decyption value from the Encyption helper
        /// </summary>
        /// <param name="value"></param>
        public string GetDecryptedValue(string value) =>
            EncryptionHelper.DecryptString(value, _inputString, _salt);
    }
}
