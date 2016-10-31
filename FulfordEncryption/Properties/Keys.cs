// Keys.cs
// by James Fulford
// Holds Settings for Engine. Can:
// Generate new Keys and verify keys.
// Later will implement Settings


using System;
using System.Collections.Generic;
using Encryption;
namespace Engine
{
	public class Keys : Settings
	{
		public int[][] assembly_key;
		public char[] alphabet_key;
		public int[] spin_key;


		// REQUIREMENTS:
		// Must ensure spin_key and assembly_key are same length.



		/* Generates new keys.
		 */
		public Keys()
		{
			//Generate random object
			Random seeder = new Random();
			Random unicorn = new Random( seeder.Next( int.MinValue, int.MaxValue ) ); //Wicked random seed
			int width = unicorn.Next( 8, 256 ); // number of rotors to have
			int character_complexity = 256; // values in ascii we accept in
			//int output_complexity = 256;  // values in ascii we put out


			// ALPHABET
			List<char> gammadel = new List<char>( character_complexity );
			for ( int i = 0 ; i < character_complexity ; i++ )
			{
				gammadel.Add( (char) i); // ascii
			}
			List<char> betagam = new List<char>( character_complexity );
			for ( int i = 0 ; i < character_complexity ; i++ )
			{
				int index = unicorn.Next( 0, gammadel.Count );
				betagam.Add( gammadel[ index ] ); // adds a random ascii value from gammadel to betagam
				gammadel.RemoveAt(index); // removes that value from gammadel to avoid repeats
			}
			alphabet_key = betagam.ToArray(); // now, it's an array of random characters
			// "alpha" + "beta" = alphabet
			// so, why not "beta" + "gamma" = betagam
			// so, why not "gamma" + "delta" = gammadel


			// ASSEMBLY KEY
			// only generates straight assembly keys
			assembly_key = new int[width][];
			for ( int i = 0 ; i < width ; i++ )
			{
				assembly_key[ i ] = new int[ character_complexity ];
				List<int> values = new List<int>();
				for ( int j = 0 ; j < character_complexity ; j++ )
				{
					values.Add( j ); //makes list of values that should be in assembly_key[i]
				}

				for ( int j = 0 ; j < character_complexity ; j++ )
				{
					int index = unicorn.Next( 0, values.Count );
					assembly_key[ i ][ j ] = values[ index ]; //adds a random value to assembly_key[i] at j.
					values.RemoveAt(index); // removes value that was used from values, to prevent repeats.
				}
			}


			// SPIN KEY
			spin_key = new int[ width ];
			for ( int i = 0 ; i < spin_key.Length ; i++ )
			{
				spin_key[ i ] = unicorn.Next( 0, alphabet_key.Length);
			}
		}


		//INTERFACE IMPLEMENTATION

		public bool isValid()
		{
			NotImplementedException temper_tantrum = new NotImplementedException( "Key verifier have not been implemented yet!" );
			throw temper_tantrum;
		}

		public Settings load( string path )
		{
			NotImplementedException temper_tantrum = new NotImplementedException( "Key loader have not been implemented yet!" );
			throw temper_tantrum;
		}

		public void dump( string path )
		{
			NotImplementedException temper_tantrum = new NotImplementedException( "Key dumper have not been implemented yet!" );
			throw temper_tantrum;
		}


		// USEFUL TESTING ACCESSORIES

		internal Keys( int[][] assembly_key, char[] alphabet_key, int[] spin_key )
		{
			this.assembly_key = assembly_key;
			this.alphabet_key = alphabet_key;
			this.spin_key = spin_key;
		}
	}
}

