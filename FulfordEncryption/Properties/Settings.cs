// Settings.cs
// /Users/jamesfulford/Projects/Enigma/Enigma/Settings.cs
//
// }}}} by jamesfulford
//     } 
//     } 9/23/2016 at 0:23
//
//
// }}}} Settings:
//     } is the abstraction of different kinds of Keys and configurations.
//     }}}} holds nothing, but subclasses should.
//     }}}} does make sure Keys and configurations can generate and validate.
//
//     } exists for abstractly returning and passing arguments from Encrypter-implementations (.set(Settings settings) and .settings())
//     } No resources currently exist for this object.
//
//
using System;
namespace Encryption
{
	public interface Settings
	{

		/* Will return whether the present settings are acceptable.
         * 
         */
		bool isValid();


		/* Saves the Settings to specified file.
		 * Overwrites file if exists; creates file if doesn't exist.
		 * Throws exceptions if directories are missing.
		 */
		void dump( string path );


		/* Imports Settings from specified file. Overwrites current Settings.
		 * Should throw exceptions if file does not exist.
		 * Should throw exceptions if file cannot be parsed.
		 * Should throw exceptions if Settings are not valid.
		 */
		Settings load( string path );


	}
}
