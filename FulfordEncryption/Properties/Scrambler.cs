// Scrambler.cs
//
// }}}} by jamesfulford
//     } james.patrick.fulford@gmail.com
//     } 10/2/2016 at 15:6
//
//
// }}}} Scrambler:
//     } is an interface 
//     }}}} sends ints to ints (encrypt)
//     }}}} and is reversible (decrypt)
//	   }}}} so long as the right settings are set.	
//
//     } exists so Encrypter can reference abstractly.
//
//
using System;
namespace Encryption
{
	public interface Scrambler
	{
		int[] encrypt( int[] message );
		int[] decrypt( int[] cipher );
		void set( int[] settings );
		int[] setting();
		object configuration();
	}
}
