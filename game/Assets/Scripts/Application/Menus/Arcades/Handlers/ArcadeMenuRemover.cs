using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.Handlers
{
    internal static class ArcadeMenuRemover
    {
        public static void Remove( ArcadeMenuUi arcadeMenu )
        {
            foreach ( var level in arcadeMenu.LevelsContainer.FindChildren() )
            {
                if ( level.name == arcadeMenu.ExampleLevel.name )
                {
                    continue;
                }
                
                Object.Destroy( level );
            }
        }
    }
}