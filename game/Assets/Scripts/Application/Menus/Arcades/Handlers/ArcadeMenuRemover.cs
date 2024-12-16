using Assets.Scripts.Application.Menus.Arcades.LevelsMenu;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.Handlers
{
    internal static class ArcadeMenuRemover
    {
        public static void Remove( LevelsMenuUi levelsMenuUi )
        {
            foreach ( var level in levelsMenuUi.LevelsContainer.FindChildren() )
            {
                if ( level.name == levelsMenuUi.ExampleLevel.name )
                {
                    continue;
                }
                
                Object.Destroy( level );
            }
            
            levelsMenuUi.LevelsContainer.SetActive( false );
        }
    }
}