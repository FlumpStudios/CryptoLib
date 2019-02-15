# CryptoLib
Simple library for added Rijndael to .NET projects

<h3>What is CryptoLib</h3>
Cyptolib is an encryption library designed to make it as simple as possible to add AES encryption to .NET core projects.

<h3>Basic setup</h3>
<p>Adding Cryptolib to a project should be pretty painless. </p>
<p>First register the service in startup.cs, passing your input key and salt to the constructor.</p>
<p><strong>services.AddTransient<ICryptoManager>(s => new CryptoManager(inputString, salt));</p></strong>
<br/>
<p><strong>Note:</strong> As with all keys, it is is recommended that your store your keys in your user secrets for development and a secure key manager 
such as AWS Secrets manager or Azure ket vault for deployment</p>
<br/>
<p>
  Now add the 'Encrypt' Attribute to the properties in the the model you wish to Encrypt/Decrypt <br/>
  
  [Encrypt]<br/>
  public int customerId { get; set; }  
</p>
<p>
  To run the cipher, simple run the RunCipher command and pass through the required model and Encryption option...like so. <br/>
  
  <strong>_cryptoManager.RunCipher(customerModel,EncryptionOptions.Encrypt); </strong>
  <br/>
  To decrypt just change the Encryption option to Decrypt
</p>
<p>
  RunCipherwill look through the passed model and look for any properties that have the Encrypt attribute attached and will encypt/decrypt them accordingly.  
</p>

<p>
 For a higher level of control you can encrypt or decrypt strings directly by using the EncyptionHelper class.
 This contains 2 public static methods <br/>
 EncryptString(string text, string inputString,  string salt) <br/>
 DecryptString(string encryptedText, string inputString, string salt) <br/>
 
 You can call these methods directly to encrypt/decypt an individual string.
 
 
</p>

