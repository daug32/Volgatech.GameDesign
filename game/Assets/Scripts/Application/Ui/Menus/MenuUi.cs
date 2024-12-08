using Assets.Scripts.Application.Ui.Arcades;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui.Menus
{
    internal class MenuUi
    {
        private readonly GameObject _gameObject;
        public readonly MainMenuUi MainMenu; 
        public readonly ArcadeMenuUi ArcadeMenu;

        public MenuUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            var menuChildrenContainer = new GameObjectChildrenContainer( gameObject );
            MainMenu = new MainMenuUi( menuChildrenContainer.Get( "main_menu" ) );   
            ArcadeMenu = new ArcadeMenuUi( menuChildrenContainer.Get( "arcade_menu" ) );
        }

        public void SetActive( bool activity )
        {
            _gameObject.SetActive( activity );
        }
    }
}