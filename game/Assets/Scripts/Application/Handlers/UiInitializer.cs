using System;
using System.Collections;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
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

            ui.Menu.MainMenu.ArcadeButtonEventManager.AddWithCommonPriority( () => ShowArcadeMenu( ui ) );
            ui.Menu.MainMenu.SandboxButtonEventManager.AddWithCommonPriority( () => LoadSandbox( ui ) );
            ui.Menu.MainMenu.ExitButtonEventManager.AddWithCommonPriority( UnityEngine.Application.Quit );
            
            ui.Menu.ArcadeMenu.GetBackButtonEventManager.AddWithCommonPriority( () => ShowMainMenu( ui ) );
            ui.Menu.ArcadeMenu.ChooseLevelEventManger.AddWithCommonPriority( levelType => LoadLevel( levelType, ui ) );
            ui.Menu.ArcadeMenu.Level.LevelCompletedEventManager.AddWithCommonPriority( completedLevel => behaviour.StartCoroutine( CompleteLevel( ui ) ) );
            ui.Menu.ArcadeMenu.Level.LevelSettings.GetToMainMenuEvenManager.AddWithCommonPriority( () => ShowMainMenu( ui ) );
            
            ui.Menu.SandboxMenu.GetToMainMenuEvenManager.AddWithCommonPriority( () => ShowMainMenu( ui ) );

            ShowMainMenu( ui );
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
            ui.Menu.ArcadeMenu.Level.Timer.SetActive( false );
            
            ui.Menu.SandboxMenu.SetActive( false );

            ui.Menu.SetActive( true );
            ui.Menu.MainMenu.SetActive( true );
        }

        private static void LoadSandbox( UserInterface ui )
        {
            ui.Menu.SetActive( true );
            ui.Menu.MainMenu.SetActive( false );
            ui.Menu.SandboxMenu.SetActive( true );
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