using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui
{
    internal class UserInterface
    {
        public readonly GameObject Canvas;
        public readonly LevelUi Level;

        public UserInterface(
            GameObject canvas,
            GameObject level )
        {
            Canvas = canvas.ThrowIfNull( nameof( canvas ) );
            Level = new LevelUi( level );
        }
    }
}