// Encoder.cs
//
// }}}} by jamesfulford
//     } james.patrick.fulford@gmail.com
//     } 10/2/2016 at 14:56
//
// }}}} Encoder:
//     } is an interface
//     }}}} converts chars to ints
//     }}}} converts ints to chars
//
//     } exists abstractly so Encrypter can call it.
//
//


namespace Encryption
{
	public interface Encoder
	{
		/* Converts letters into numbers. Inverse process is decode.
		 */
		int[] encode( char[] letters );


		/* Converts numbers into letters. Inverse process is encode.
		 */
		char[] decode( int[] numbers );


		/* Returns the current mapping of the encoder.
		 */
		char[] get_mapping();
	}
}
