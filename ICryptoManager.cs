/*--------------------------------------------------------------------------------*
            CryptoLib Rijndael Encyptions Library - By Paul Marrable
            
          This libary is free to use but please leave this comment here :)
*---------------------------------------------------------------------------------*/


namespace CryptoLib
{
    public interface ICryptoManager
    {
        T RunCipher<T>(T model, EncryptionOptions encryptionOption);
    }
}