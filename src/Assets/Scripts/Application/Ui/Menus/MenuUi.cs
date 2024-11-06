using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Ui
{
    internal class MenuUi
    {
        private readonly GameObject _gameObject;
        public readonly MainMenuUi MainMenu; 
        public readonly ArcadeMenuUi ArcadeMenu;

        public MenuUi( GameObject gameObject )
        {
            var menuChildrenContainer = new GameObjectChildrenContainer( gameObject );
            MainMenu = new MainMenuUi( menuChildrenContainer.Get( "main_menu" ) );   
            ArcadeMenu = new ArcadeMenuUi( menuChildrenContainer.Get( "arcade_menu" ) );
        }
    }
}