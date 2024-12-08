using System;
using System.Linq;

namespace Assets.Scripts.Application.Levels.Extensions
{
    internal static class LevelTypeExtensions 
    {
        public static LevelType? GetPreviousLevel( this LevelType levelType )
        {
            var newLevel = ( LevelType )( ( int )levelType + 1 );
            return Enum.IsDefined( typeof( LevelType ), newLevel ) ? newLevel : null;
        }
        
        public static string ToDatabaseFilename( this LevelType levelType ) => levelType.ToString().ToLower();
        public static int ToLevelNumber( this LevelType levelType ) => Int32.Parse( levelType.ToString().Split( '_' ).Last() ) + 1;
    }
}
