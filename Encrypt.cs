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

    /// <summary>
    /// Custom attribute, add [EncryptClass] attribute to any class within model property to mark for encryption
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptClassAttribute : Attribute { }

    /// <summary>
    /// Custom attribute, add [EncryptCollection] attribute to any model enumerable property to mark for encryption
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptCollectionAttribute : Attribute { }

}
