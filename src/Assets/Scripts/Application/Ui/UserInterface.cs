using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui
{
    internal class UserInterface
    {
        public readonly GameObject Canvas;

        public readonly GameObject Level;
        public readonly GameObject SuccessText;
        public readonly GameObject TargetsContainer;

        public UserInterface(
            GameObject canvas,
            GameObject level, 
            GameObject targetsContainer )
        {
            Canvas = canvas.ThrowIfNull( nameof( canvas ) );
            Level = level.ThrowIfNull( nameof( level ) );
            SuccessText = level.FindChild( "success_text" ).ThrowIfNull( message: "Failed to find success_text game object" );
            TargetsContainer = targetsContainer.ThrowIfNull( nameof( targetsContainer ) );
        }
    }
}