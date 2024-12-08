using Assets.Scripts.Application.Menus;
using Assets.Scripts.Application.Menus.Arcades.Levels;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application
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