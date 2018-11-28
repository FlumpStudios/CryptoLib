/*--------------------------------------------------------------------------------*
            CryptoLib Rijndael Encyptions Library - By Paul Marrable
            
          This libary is free to use but please leave this comment here :)
*---------------------------------------------------------------------------------*/


using System;


namespace CryptoLib
{
    /// <summary>
    /// Custom attribute, add [Encrypt] attribute to any model property to mark for encryption
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptAttribute : Attribute { }    
}
