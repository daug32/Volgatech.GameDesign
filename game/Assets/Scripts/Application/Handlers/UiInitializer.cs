using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Repositories;
using UnityEngine;

namespace Assets.Scripts.Application.Handlers
{
    public static class UiInitializer
    {
        public static void Initialize( MonoBehaviour behaviour )
        {
            UserInterface ui = UiItemsRepository.GetUserInterface();

            ui.Menu.MainMenu.OnOpenArcadeEvent.AddWithCommonPriority( () => ShowArcadeMenu( ui ) );
            ui.Menu.MainMenu.OnOpenSandboxEvent.AddWithCommonPriority( () => LoadSandbox( ui ) );
            ui.Menu.MainMenu.OnExitEvent.AddWithCommonPriority( UnityEngine.Application.Quit );
            
            ui.Menu.ArcadeMenu.OnOpenMainMenuEvent.AddWithCommonPriority( () => ShowMainMenu( ui ) );
            ui.Menu.ArcadeMenu.OnSelectLevelEvent.AddWithCommonPriority( levelType => LoadLevel( levelType, ui ) );
            
            ui.Menu.SandboxMenu.OnOpenMainMenuEvent.AddWithCommonPriority( () => ShowMainMenu( ui ) );
            
            ShowMainMenu( ui );
        }

        private static void ShowArcadeMenu( UserInterface ui )
        {
            LevelDataRepository.Load();
            
            ui.Menu.ArcadeMenu.SetActive( true );
            ui.Menu.ArcadeMenu.Level.LevelSettings.Hide();
            ui.Menu.ArcadeMenu.Level.UnloadLevel();

            ui.Menu.SetActive( true );
            ui.Menu.MainMenu.SetActive( false );
        }

        private static void ShowMainMenu( UserInterface ui )
        {
            ui.Menu.ArcadeMenu.SetActive( false );
            ui.Menu.ArcadeMenu.Level.LevelSettings.Hide();
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
            ui.Menu.ArcadeMenu.Level.LevelSettings.Hide();
            ui.Menu.ArcadeMenu.Level.LoadLevel( levelType );

            ui.Menu.SetActive( true );
            ui.Menu.MainMenu.SetActive( false );
        }
    }
}