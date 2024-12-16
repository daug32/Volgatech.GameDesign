using Assets.Scripts.Application.Menus.Arcades.Handlers;
using Assets.Scripts.Application.Menus.Arcades.Levels;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Arcades
{
    internal class ArcadeMenuUi
    {
        private readonly GameObject _gameObject;

        public readonly LevelUi Level;

        public readonly GameObject LevelsContainer;
        public readonly GameObject ExampleLevel;
        
        public readonly EventManager OnOpenMainMenuEvent = new();
        public readonly EventManager<LevelType> OnSelectLevelEvent = new();
        
        public ArcadeMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );
            var childContainer = new GameObjectChildrenContainer( gameObject );

            Level = new LevelUi( childContainer.Get( "level" ) );
            Level.OnOpenMainMenuEvent.AddWithCommonPriority( OnOpenMainMenuEvent.Trigger );

            var menuContainer = new GameObjectChildrenContainer( childContainer.Get( "menu_container" ) );
            menuContainer.Get( "back_button" ).GetComponent<Button>().onClick.AddListener( OnOpenMainMenuEvent.Trigger );
            LevelsContainer = menuContainer.Get( "levels" );
            ExampleLevel = LevelsContainer.FindChild( "example_level" );
        }

        public void SetActive( bool activity )
        {
            _gameObject.SetActive( activity );

            if ( activity )
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