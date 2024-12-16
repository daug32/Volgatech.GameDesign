using Assets.Scripts.Application.Menus.Arcades;
using Assets.Scripts.Application.Menus.MainMenus;
using Assets.Scripts.Application.Menus.Researches;
using Assets.Scripts.Application.Menus.Sandbox;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus
{
    internal class MenuUi
    {
        public readonly MainMenuUi MainMenu;
        public readonly SandboxMenuUi SandboxMenu;
        public readonly ArcadeMenuUi ArcadeMenu;
        public readonly ResearchesMenuUi ResearchesMenu;

        public MenuUi( GameObject gameObject )
        {
            var menuChildrenContainer = new GameObjectChildrenContainer( gameObject );

            MainMenu = new MainMenuUi( menuChildrenContainer.Get( "main_menu" ) );
            MainMenu.OnOpenArcadeEvent.AddWithCommonPriority( OpenArcade );
            MainMenu.OnOpenSandboxEvent.AddWithCommonPriority( OpenSandbox );
            MainMenu.OnOpenResearchesMenuEvent.AddWithCommonPriority( OpenResearchesMenu );
            
            ArcadeMenu = new ArcadeMenuUi( menuChildrenContainer.Get( "arcade_menu" ) );
            ArcadeMenu.OnOpenMainMenuEvent.AddWithCommonPriority( OpenMainMenu );
            
            SandboxMenu = new SandboxMenuUi( menuChildrenContainer.Get( "sandbox_menu" ) );
            SandboxMenu.OnOpenMainMenuEvent.AddWithCommonPriority( OpenMainMenu );
            
            ResearchesMenu = new ResearchesMenuUi( menuChildrenContainer.Get( "researches_menu" ) );
            ResearchesMenu.OnOpenMainMenuEvent.AddWithCommonPriority( OpenMainMenu );
        }

        public void OpenMainMenu()
        {
            MainMenu.SetActive( true );
            ArcadeMenu.SetActive( false );
            SandboxMenu.SetActive( false );
            ResearchesMenu.SetActive( false );
        }

        private void OpenArcade()
        {
            MainMenu.SetActive( false );
            ArcadeMenu.SetActive( true );
            SandboxMenu.SetActive( false );
            ResearchesMenu.SetActive( false );
        }

        private void OpenSandbox()
        {
            MainMenu.SetActive( false );
            ArcadeMenu.SetActive( false );
            SandboxMenu.SetActive( true );
            ResearchesMenu.SetActive( false );
        }

        private void OpenResearchesMenu()
        {
            MainMenu.SetActive( false );
            ArcadeMenu.SetActive( false );
            SandboxMenu.SetActive( false );
            ResearchesMenu.SetActive( true );
        }
    }
}