// Alphabet.cs
// by James Fulford
// Maps chars to ints.

using System.Collections.Generic;
using Encryption;

namespace Engine
{
	internal class Alphabet : Encoder
	{
		private char[] mapping;

		public char[] get_mapping()
		{
			char[] ret_map = new char[ mapping.Length ];
			mapping.CopyTo( ret_map , 0);
			return ret_map;
		}

		/* Constructs an alphabet from string
		 * Removes all duplicate letters
		 */
		public Alphabet( string char_mapping )
		{
			char[] raw_map = char_mapping.ToCharArray();
			List<char> uniques = new List<char>();
			foreach ( char letter in raw_map )
			{
				if ( !uniques.Contains( letter ) )
				{
					uniques.Add( letter );
				}
			}
			this.mapping = uniques.ToArray();
		}

		/* Constructs an alphabet from char[]
		 * Removes all duplicate letters
		 */
		public Alphabet( char[] char_mapping )
		{
			List<char> uniques = new List<char>();
			foreach ( char letter in char_mapping )
			{
				if ( !uniques.Contains( letter ) )
				{
					uniques.Add( letter );
				}
			}
			this.mapping = uniques.ToArray();
		}

		public int[] encode( char[] letters )
		{
			int[] encoded = new int[ letters.Length ];
			for ( int i = 0 ; i < letters.Length ; i++ )
			{
				// this next part is basically encoded[i] = this.mapping.index(letters[i]);
				// alas, arrays do not have the function .index
				for ( int j = 0 ; j < this.mapping.Length ; j++ )
				{
					if ( letters[ i ].Equals( this.mapping[ j ] ) )
					{
						encoded[ i ] = j; // returns the index in the mapping where this letter is
						break;
					}
				}
			}
			return encoded;
		}

		public char[] decode( int[] numbers )
		{
			char[] decoded = new char[ numbers.Length ];
			for ( int i = 0 ; i < numbers.Length ; i++ )
			{
				decoded[ i ] = this.mapping[ numbers[ i ] ]; // returns the letter in this index
			}
			return decoded;
		}

	}
}

