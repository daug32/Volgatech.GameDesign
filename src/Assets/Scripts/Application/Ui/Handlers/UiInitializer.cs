using System;
using System.Collections;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Events;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui.Handlers
{
    public static class UiInitializer
    {
        public static void Initialize( MonoBehaviour behaviour )
        {
            UserInterface userInterface = UiItemsRepository.GetUserInterface();

            LevelCompletedEventManager.Add( () => behaviour.StartCoroutine( CompleteLevel( userInterface ) ) );

            userInterface.Menu.MainMenu.ArcadeButtonEventManager.AddWithCommonPriority( () => ShowArcadeMenu( userInterface ) );
            userInterface.Menu.MainMenu.ExitButtonEventManager.AddWithCommonPriority( UnityEngine.Application.Quit );

            userInterface.Menu.ArcadeMenu.GetBackButtonEventManager.AddWithCommonPriority( () => ShowMainMenu( userInterface ) );
            userInterface.Menu.ArcadeMenu.ChooseLevelEventManger.AddWithCommonPriority( levelType => LoadLevel( levelType, userInterface ) );

            userInterface.Level.OpenLevelSettingsEventManager.AddWithCommonPriority( () => OpenLevelSettings( userInterface ) );
            userInterface.Level.LevelSettings.GetToLevelEventManager.AddWithCommonPriority( () => HideLevelSettings( userInterface ) );
            userInterface.Level.LevelSettings.GetToMainMenuEvenManager.AddWithCommonPriority( () => ShowMainMenu( userInterface ) );

            ShowMainMenu( userInterface );
        }

        private static void OpenLevelSettings( UserInterface userInterface )
        {
            userInterface.Level.LevelSettings.ShowSettings(
                userInterface.Level.CurrentLevel.ThrowIfNull( message: "Level is not loaded" ) );
            // TODO: Block elements reactions
        }

        private static void HideLevelSettings( UserInterface userInterface )
        {
            userInterface.Level.LevelSettings.HideSettings();
        }

        private static void ShowArcadeMenu( UserInterface userInterface )
        {
            userInterface.Level.LevelSettings.HideSettings();
            userInterface.Level.UnloadLevel();
            userInterface.Menu.SetActive( true );
            userInterface.Menu.MainMenu.SetActive( false );
            userInterface.Menu.ArcadeMenu.SetActive( true );
        }

        private static void ShowMainMenu( UserInterface userInterface )
        {
            userInterface.Level.LevelSettings.HideSettings();
            userInterface.Level.UnloadLevel();
            userInterface.Menu.SetActive( true );
            userInterface.Menu.MainMenu.SetActive( true );
            userInterface.Menu.ArcadeMenu.SetActive( false );
        }

        private static void LoadLevel( LevelType levelType, UserInterface userInterface )
        {
            userInterface.Level.LevelSettings.HideSettings();
            userInterface.Level.LoadLevel( levelType );
            userInterface.Menu.SetActive( false );
            userInterface.Menu.MainMenu.SetActive( false );
            userInterface.Menu.ArcadeMenu.SetActive( false );
        }

        private static IEnumerator CompleteLevel( UserInterface userInterface )
        {
            yield return userInterface.Level.CompleteLevel();
            
            LevelType nextLevel = GetNextLevel( userInterface.Level.CurrentLevel );
            userInterface.Level.UnloadLevel();
            LoadLevel( nextLevel, userInterface );
        }
        
        private static LevelType GetNextLevel( LevelType? currentLevel )
        {
            LevelType level = currentLevel.ThrowIfNull( message: "Level was not loaded" );
            int nextLevel = ( int )level + 1;

            return Enum.IsDefined( typeof( LevelType ), nextLevel ) 
                ? ( LevelType )nextLevel 
                : LevelType.Level_0;
        }
    }
}