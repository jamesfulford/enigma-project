// Assembly.cs
// by James Fulford
// Has an array of rotors and the pattern to follow for spinning them.
// Can encrypt and decrypt arrays of integers, and scramble settings.
// Can also configure spin settings in order to synchronize with another user.

using System;
using System.Collections.Generic;
using Encryption;

namespace Engine
{
	internal class Assembly : Scrambler
	{
		Rotor[] rotors;

		/* Initializes an assembly based on:
		 * Spin_key: how each rotor 
		 */
		internal Assembly( int[][] assembly_key, int[] spin_key )
		{
			rotors = new Rotor[ assembly_key.Length ];
			for ( int i = 0 ; i < assembly_key.Length ; i++ )
			{
				rotors[ i ] = new Rotor( assembly_key[ i ], spin_key[ i ] );
			}
		}


		// SCRAMBLER INTERFACE

		/* Sets the spin of the rotors to this value.
		 * 
		 */
		public void set( int[] settings )
		{
			for ( int i = 0 ; i < settings.Length ; i++ )
			{
				rotors[ i ].set_spin( settings[ i ] );
			};
		}

		/* Encrypts integer array message by passing it through the rotors.
		 * Spins the rotors for every number.
		 */
		public int[] encrypt( int[] message )
		{
			int[] cipher = new int[ message.Length ];
			for ( int i = 0 ; i < message.Length ; i++ )
			{
				cipher[ i ] = message[ i ];
				for ( int j = 0 ; j < rotors.Length ; j++ )
				{
					cipher[ i ] = rotors[ j ].encrypt( cipher[ i ] );
				}
				this.forward();
			}
			return cipher;
		}

		/* Decrypts integer array cipher by passing it through the rotors backwards.
		 * Spins the rotors for every number.
		 */
		public int[] decrypt( int[] cipher )
		{
			int[] message = new int[ cipher.Length ];
			for ( int i = 0 ; i < cipher.Length ; i++ )
			{
				message[ i ] = cipher[ i ];
				for ( int j = rotors.Length - 1 ; j > -1 ; j-- )
				{
					message[ i ] = rotors[ j ].decrypt( message[ i ] );
				}
				this.forward();
			}
			return message;
		}

		public int[] setting()
		{
			int[] settings = new int[ rotors.Length ];
			for ( int i = 0 ; i < rotors.Length ; i++ )
			{
				settings[ i ] = rotors[ i ].get_setting();
			}
			return settings;

		}


		// ASSEMBLY SPECIFIC METHODS

		public object configuration()
		{
			int[][] ensemble = new int[ rotors.Length ][];
			for ( int i = 0 ; i < rotors.Length ; i++ )
			{
				int[] rotary = rotors[ i ].get_rotor();
				ensemble[ i ] = new int[ rotary.Length ];
				for ( int j = 0 ; j < rotary.Length ; j++ )
				{
					ensemble[ i ][j] = rotary[j];
				}
			}
			return (object) ensemble;
		}



		/* Spins the first rotor forward once. If that rotor spins all the way around, then the next rotor spins.
		 * 
		 */
		internal void forward()
		{
			for ( int i = 0 ; i < rotors.Length ; i++ )
			{
				rotors[ i ].spin( 1 );
				if ( rotors[ i ].get_setting() != 0 )
				{
					break;
				}
			}
		}

		/* Spins the first rotor backwards once. If that rotor spins all the way around, then the next rotor spins.
		 * 
		 */
		internal void backward()
		{
			for ( int i = 0 ; i < rotors.Length ; i++ )
			{
				rotors[ i ].spin( -1 );
				if ( rotors[ i ].get_setting() != 0 )
				{
					break;
				}
			}
		}








		internal class Rotor
		{
			private int[] map;
			private int setting; //this number is in [0, map.Length) using interval notation

			public Rotor( int[] map, int setting )
			{
				this.map = map;
				this.setting = 0; // changed in the next line.
				this.set_spin( setting );
			}

			public int get_setting()
			{
				return setting;
			}

			public int[] get_rotor()
			{
				int[] mappy = new int[ map.Length ];
				for ( int i = 0 ; i < map.Length ; i++ )
				{
					mappy[ i ] = map[ i ];
				}

				int sett = setting;
				for ( int i = 0 ; i < ( mappy.Length - sett ) % mappy.Length ; i++ )
				{
					int end = mappy[ mappy.Length - 1 ];
					for ( int j = mappy.Length - 1 ; j > 0 ; j-- )
					{
						mappy[ j ] = mappy[ j - 1 ];
					}
					mappy[ 0 ] = end;
					sett = ( sett + 1 ) % mappy.Length;
				}

				return mappy;
			}

			/* Encrypts int message by passing through the rotor's mapping.
			 * Encryption can have multiple answers for the same input.
			 */
			public int encrypt( int message )
			{
				List<int> values = new List<int>(); // list of all indices that are valid responses
				for ( int i = 0 ; i < this.map.Length ; i++ )
				{
					if ( this.map[ i ].Equals( message ) )
					{
						values.Add( i ); // finding and adding all values that are valid
					}
				}
				Random chooser = new Random();
				try
				{
					int chosen = chooser.Next( 0, values.Count ); //choosing a random index
					int value = values[ chosen ]; // picking a random value in the valid responses list
					return value; // returns random value of the valid values.
				}
				catch ( ArgumentOutOfRangeException up )
				{
					Console.WriteLine( "Could not find any entry in Rotor that is " + message );
					throw up;
				}


			}

			/* Decrypts int cipher by passing through the rotor's mapping backwards.
			 * Unline encrypt, decrypt passes the vertical line test.
			 */
			public int decrypt( int cipher )
			{
				int value = this.map[ cipher ];
				return value;
			}

			/* Spins the rotor until .setting is new_setting
			 * 
			 */
			public void set_spin( int new_setting )
			{
				while ( this.setting % map.Length != new_setting )
				{
					this.spin( 1 ); // this will change the setting, too!
				}
			}

			/* Moves entries in the mapping "times" times.
			 * Can handle positive and negative integers, and 0. Really big ints are simlified by modulus.
			 */
			public void spin( int times )
			{
				if ( times > 0 ) //spinning forwards
				{
					for ( int i = 0 ; i < times % map.Length ; i++ ) // modulus simplifies really large spin requests.
					{
						int end = map[ map.Length - 1 ];
						for ( int j = map.Length - 1 ; j > 0 ; j-- )
						{
							map[ j ] = map[ j - 1 ];
						}
						map[ 0 ] = end;
					}
					setting = ( setting + 1 ) % map.Length;
				}
				else if ( times < 0 ) //spinning backwards
				{
					for ( int i = 0 ; i < times % map.Length ; i++ ) // modulus simplifies really large spin requests.
					{
						int front = map[ 0 ];
						for ( int j = 0 ; j < map.Length - 1 ; j++ )
						{
							map[ i ] = map[ i + 1 ];
						}
						map[ map.Length - 1 ] = front;
					}
					setting = ( setting - 1 ) % map.Length;
				}
				//does nothing if times is 0. Duh.
			}



		}

	}
}

