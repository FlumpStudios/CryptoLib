# CryptoLib
Library for adding AES(Rijndael) Encryption to .NET projects

<h3>What is CryptoLib</h3>
Cyptolib is an encryption library designed to make it as simple as possible to add data at rest protection to .NET core projects via AES encryption.

<h3>Basic setup</h3>
<p>Adding Cryptolib to a project should be pretty painless. </p>
<h4>With DI</h4>
<p>First register the service in startup.cs, passing your input key and salt to the constructor.</p>
<p><strong>services.AddTransient<ICryptoManager>(s => new CryptoManager(inputString, salt));</p></strong>
<h4>The old fashioned way</h4>
<p>Just import the library, new up an instance and pass the input string and salt to the constructor</p>
<code>
	var cryptoManager = new CryptoManager("Some Input String", "Some Salt");
</code>

<br/>
<p><strong>Note:</strong> As with all keys, it is is recommended that your store your keys in your user secrets for development and a secure key manager 
such as AWS Secrets manager or Azure key vault for deployment</p>

<h3>How to encrypt and decrypt strings</h3>
<p>
  cryptoLib uses reflection to find Encryption attributes added to properties in a passed model. Simply add the 'Encrypt' Attribute to the properties in the the model you wish to Encrypt/Decrypt <br/>
  
  <code>
  [Encrypt]<br/>
  public int customerId { get; set; }  
  </code>

<p>
	If the class the property is contained in is located inside another class, then you will need add an EncryptClass Attribute to the parent prop to let cryptoLib know the class contains encypted properties.
	</br>
	<code>
		[EncryptClass]</br>
        public Customer Customer { get; set; }
	</code>
</p>
  
  <p>
	If the class the property is contained in is located inside an array or collection you will need to add an EncryptCollection attribute to the collection prop, 
	</br>
	<code>
		[EncryptCollection]</br>
        public IEnumerable<Customer> Customer { get; set; }
	</code>
</p>
  
  
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
To encrypt or decrypt a string directly, you can call the GetEncryptedValue(string) GetDecryptedValue(string) directly.
</p>

<p>
 You can also call the cipher directly by using the EncyptionHelper class, this is useful for static and extension methods.
 The EncyptionHelper class contains 2 public static methods <br/>
 EncryptString(string text, string inputString, string salt) <br/>
 DecryptString(string encryptedText, string inputString, string salt) <br/>
</p>

