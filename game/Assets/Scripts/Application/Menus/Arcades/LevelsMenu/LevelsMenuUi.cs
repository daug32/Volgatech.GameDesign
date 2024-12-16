using Assets.Scripts.Application.Menus.Arcades.Handlers;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.LevelsMenu
{
    internal class LevelsMenuUi
    {
        private readonly GameObject _gameObject;

        public readonly GameObject LevelsContainer;
        public readonly GameObject ExampleLevel;
        
        public readonly EventManager<LevelType> OnSelectLevelEvent = new();
        public readonly EventManager OnOpenMainMenuEvent = new();

        public LevelsMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject;

            var childManager = new GameObjectChildrenContainer( gameObject );
            LevelsContainer = childManager.Get( "levels" );
            ExampleLevel = LevelsContainer.FindChild( "example_level" );
            OnOpenMainMenuEvent.SubscribeOnClick( childManager.Get( "back_button" ) );
        }

        public void SetActive( bool isActive )
        {
            _gameObject.SetActive( isActive );

            if ( isActive )
            {
                ArcadeMenuDrawer.Draw( this );
            }
            else
            {
                ArcadeMenuRemover.Remove( this );
            }
        }
    }
}