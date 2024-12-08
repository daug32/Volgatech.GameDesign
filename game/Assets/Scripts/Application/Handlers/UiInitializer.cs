using System;
using System.Collections;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Events;
using Assets.Scripts.Application.Menus.Arcades.Repositories;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Handlers
{
    public static class UiInitializer
    {
        public static void Initialize( MonoBehaviour behaviour )
        {
            UserInterface ui = UiItemsRepository.GetUserInterface();

            LevelCompletedEventManager.Add( () => behaviour.StartCoroutine( CompleteLevel( ui ) ) );

            ui.Menu.MainMenu.ArcadeButtonEventManager.AddWithCommonPriority( () => ShowArcadeMenu( ui ) );
            ui.Menu.MainMenu.ExitButtonEventManager.AddWithCommonPriority( UnityEngine.Application.Quit );

            ui.Menu.ArcadeMenu.GetBackButtonEventManager.AddWithCommonPriority( () => ShowMainMenu( ui ) );
            ui.Menu.ArcadeMenu.ChooseLevelEventManger.AddWithCommonPriority( levelType => LoadLevel( levelType, ui ) );
            ui.Menu.ArcadeMenu.Level.OpenLevelSettingsEventManager.AddWithCommonPriority( () => OpenLevelSettings( ui ) );
            ui.Menu.ArcadeMenu.Level.LevelSettings.GetToLevelEventManager.AddWithCommonPriority( () => HideLevelSettings( ui ) );
            ui.Menu.ArcadeMenu.Level.LevelSettings.GetToMainMenuEvenManager.AddWithCommonPriority( () => ShowMainMenu( ui ) );

            ShowMainMenu( ui );
        }

        private static void OpenLevelSettings( UserInterface ui )
        {
            ui.Menu.ArcadeMenu.Level.Statistics.Pause();
            LevelType currentLevel = ui.Menu.ArcadeMenu.Level.CurrentLevel.ThrowIfNull( message: "Level is not loaded" );
            ui.Menu.ArcadeMenu.Level.LevelSettings.ShowSettings( currentLevel );
            ui.Menu.ArcadeMenu.Level.SetElementsInteractionsBlock( true );
        }

        private static void HideLevelSettings( UserInterface ui )
        {
            ui.Menu.ArcadeMenu.Level.LevelSettings.HideSettings();
            ui.Menu.ArcadeMenu.Level.SetElementsInteractionsBlock( false );
            ui.Menu.ArcadeMenu.Level.Statistics.Resume();
        }

        private static void ShowArcadeMenu( UserInterface ui )
        {
            LevelDataRepository.Load();
            
            ui.Menu.ArcadeMenu.SetActive( true );
            ui.Menu.ArcadeMenu.Level.LevelSettings.HideSettings();
            ui.Menu.ArcadeMenu.Level.UnloadLevel();

            ui.Menu.SetActive( true );
            ui.Menu.MainMenu.SetActive( false );
        }

        private static void ShowMainMenu( UserInterface ui )
        {
            ui.Menu.ArcadeMenu.SetActive( false );
            ui.Menu.ArcadeMenu.Level.LevelSettings.HideSettings();
            ui.Menu.ArcadeMenu.Level.UnloadLevel();

            ui.Menu.SetActive( true );
            ui.Menu.MainMenu.SetActive( true );
        }

        private static void LoadLevel( LevelType levelType, UserInterface ui )
        {
            ui.Menu.ArcadeMenu.SetActive( false );
            ui.Menu.ArcadeMenu.Level.LevelSettings.HideSettings();
            ui.Menu.ArcadeMenu.Level.LoadLevel( levelType );

            ui.Menu.SetActive( true );
            ui.Menu.MainMenu.SetActive( false );
        }

        private static IEnumerator CompleteLevel( UserInterface ui )
        {
            yield return ui.Menu.ArcadeMenu.Level.CompleteLevel();
            LevelType nextLevel = GetNextLevel( ui.Menu.ArcadeMenu.Level.CurrentLevel.ThrowIfNull( message: "Level was not loaded" ) );
            ui.Menu.ArcadeMenu.Level.UnloadLevel();
            LoadLevel( nextLevel, ui );
        }
        
        private static LevelType GetNextLevel( LevelType currentLevel )
        {
            int nextLevel = ( int )currentLevel + 1;

            return Enum.IsDefined( typeof( LevelType ), nextLevel ) 
                ? ( LevelType )nextLevel 
                : LevelType.Level_0;
        }
    }
}