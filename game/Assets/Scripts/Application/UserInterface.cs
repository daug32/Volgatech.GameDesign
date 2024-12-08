using Assets.Scripts.Application.Menus;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application
{
    // TODO: Add localization
    internal class UserInterface
    {
        public readonly GameObject Canvas;
        public readonly MenuUi Menu;

        public UserInterface( GameObject canvas )
        {
            Canvas = canvas.ThrowIfNull( nameof( canvas ) );
            Menu = new MenuUi( Canvas.FindChild( "menu" ) );
        }
    }
}