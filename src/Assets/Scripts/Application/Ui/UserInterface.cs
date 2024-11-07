using Assets.Scripts.Application.Ui.Levels;
using Assets.Scripts.Application.Ui.Menus;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui
{
    // TODO: Add localization
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
    }
}