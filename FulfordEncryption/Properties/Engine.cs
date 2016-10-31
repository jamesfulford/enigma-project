// Engine.cs
// by James Fulford
// Encrypts and decrypts messages. 
// Must have the right settings in order to work properly, which can imported from Keys.

using Encryption;

namespace Engine
{
	public class Engine : Encrypter
	{
		/* Constructs an Enigma machine with keys.
		 * Use .encrypt and .decrypt to use.
		 */
		public Engine( Settings settings )
		{
			this.set( settings );
		}

		/* Sets the enigma machine's rotors to the given position.
		 */
		public override void set( Settings settings )
		{
			// Downcasting from settings (loss of generality)
			Keys keys = (Keys) settings;

			// CREATING DEEP COPIES to avoid side effects.
			int[][] assem = new int[ keys.assembly_key.Length ][]; // assembly_key copy
			for ( int i = 0 ; i < assem.Length ; i++ )
			{
				assem[ i ] = new int[ keys.assembly_key[ i ].Length ];
				for ( int j = 0 ; j < keys.assembly_key[ i ].Length ; j++ )
				{
					assem[ i ][ j ] = keys.assembly_key[ i ][ j ];
				}
			}

			int[] spink = new int[ keys.spin_key.Length ]; // spin_key copy
			for ( int i = 0 ; i < keys.spin_key.Length ; i++ )
			{
				spink[ i ] = keys.spin_key[ i ];
			}

			char[] alf = new char[ keys.alphabet_key.Length ]; // alphabet_key copy
			for ( int i = 0 ; i < keys.alphabet_key.Length ; i++ )
			{
				alf[ i ] = keys.alphabet_key[ i ];
			}

			// Now, we build:
			scrambler = new Assembly( assem, spink );
			encoder = new Alphabet( alf );

		}

		public override Settings getSettings()
		{
			int[][] assemble_key = (int[][]) scrambler.configuration();
			int[] set_key = (int[]) scrambler.setting();

			char[] alphabet_key = (char[]) encoder.get_mapping();

			return new Keys( assemble_key, alphabet_key, set_key );
		}

	}
}