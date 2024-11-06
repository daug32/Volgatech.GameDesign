using System;
using System.Collections;
using Assets.Scripts.Repositories.Ui;
using UnityEngine;

namespace Assets.Scripts.Application.Levels.Handlers
{
    internal static class LevelCompleter
    {
        public static IEnumerator Complete()
        {
            var userInterface = UiItemsRepository.GetUserInterface();

            yield return userInterface.Level.CompleteLevel();
            
            LevelUnloader.Unload();
            LevelLoader.Initialize( ChooseNextLevel( LevelLoader.CurrentLevel ) );
        }

        private static LevelType ChooseNextLevel( LevelType finishedLevel )
        {
            int level = ( int )finishedLevel + 1;
            LevelType nextLevel = ( LevelType )level;
            if ( !Enum.IsDefined( typeof( LevelType ), nextLevel ) )
            {
                return LevelType.Level_0;
            }

            return nextLevel;
        }
    }
}