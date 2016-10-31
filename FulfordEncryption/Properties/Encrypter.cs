// Encrypter.cs
// by James Fulford
// Abstract class
// Encrypts and decrypts messages.


namespace Encryption
{
	public abstract class Encrypter
	{
		internal Encoder encoder;
		internal Scrambler scrambler;

		/* Encrypts a string into cipher text.
		 */
		public string encrypt( string message )
		{
			char[] chars = message.ToCharArray();
			int[] numbers = encoder.encode( chars );
			numbers = scrambler.encrypt( numbers );
			char[] cipher = encoder.decode( numbers );
			return new string( cipher );
		}

		/* Undoes the encrypt function if settings are the same
		 */
		public string decrypt( string cipher )
		{
			char[] chars = cipher.ToCharArray();
			int[] numbers = encoder.encode( chars );
			numbers = scrambler.decrypt( numbers );
			char[] message = encoder.decode( numbers );
			return new string( message );
		}

		/* Changes the settings to the provided specifications
         */
		public abstract void set( Settings settings );

		/* Retrieves a copy of the settings.
		 */
		public abstract Settings getSettings();
	}
}
