/*--------------------------------------------------------------------------------*
            CryptoLib Rijndael Encyptions Library - By Paul Marrable
            
          This libary is free to use but please leave this comment here :)
*---------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace CryptoLib
{
    public interface ICryptoManager
    {
        T RunCipher<T>(T model, EncryptionOptions encryptionOption);
        IEnumerable<T> RunCipher<T>(IEnumerable<T> modelList, EncryptionOptions encryptionOption);
        string GetEncryptedValue(string value);
        string GetDecryptedValue(string value);
    }
}