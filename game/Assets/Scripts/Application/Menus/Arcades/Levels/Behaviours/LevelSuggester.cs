using System;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Behaviours
{
    internal static class LevelSuggester
    {
        public static LevelType? SuggestNextLevel( LevelType currentLevel )
        {
            int nextLevel = ( int )currentLevel + 1;

            return Enum.IsDefined( typeof( LevelType ), nextLevel ) 
                ? ( LevelType )nextLevel 
                : null;
        }
    }
}