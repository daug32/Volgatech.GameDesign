using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui
{
    internal class UserInterface
    {
        public readonly GameObject Canvas;

        public readonly LevelUi Level;
        public readonly MenuUi Menu;

        public UserInterface( GameObject canvas )
        {
            Canvas = canvas.ThrowIfNull( nameof( canvas ) );
            
            var childrenContainer = new GameObjectChildrenContainer( canvas );
            Level = new LevelUi( childrenContainer.Get( "level" ) );
            Menu = new MenuUi( childrenContainer.Get( "menu" ) );
        }

        public void ShowMainMenu()
        {
            Level.SetActive( false );
            Menu.ArcadeMenu.SetActive( false );
            Menu.MainMenu.SetActive( true );
        }

        public void ShowArcadeMenu()
        {
            Level.SetActive( false );
            Menu.MainMenu.SetActive( false );
            Menu.ArcadeMenu.SetActive( true );
        }
    }
}