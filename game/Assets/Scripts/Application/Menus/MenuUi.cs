using Assets.Scripts.Application.Menus.Arcades;
using Assets.Scripts.Application.Menus.MainMenus;
using Assets.Scripts.Application.Menus.Sandbox;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus
{
    internal class MenuUi
    {
        private readonly GameObject _gameObject;
        public readonly MainMenuUi MainMenu;
        public readonly SandboxMenuUi SandboxMenu;
        public readonly ArcadeMenuUi ArcadeMenu;

        public MenuUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            var menuChildrenContainer = new GameObjectChildrenContainer( gameObject );
            MainMenu = new MainMenuUi( menuChildrenContainer.Get( "main_menu" ) );   
            ArcadeMenu = new ArcadeMenuUi( menuChildrenContainer.Get( "arcade_menu" ) );
            SandboxMenu = new SandboxMenuUi( menuChildrenContainer.Get( "sandbox_menu" ) );
        }

        public void SetActive( bool activity )
        {
            _gameObject.SetActive( activity );
        }
    }
}