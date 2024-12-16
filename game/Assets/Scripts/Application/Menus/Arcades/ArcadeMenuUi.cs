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
        public readonly EventManager GetBackButtonEventManager = new();
        public readonly EventManager<LevelType> ChooseLevelEventManger = new();

        public readonly LevelUi Level;

        private readonly GameObjectChildrenContainer _menuContainer;
        public readonly GameObject LevelsContainer;
        public readonly GameObject ExampleLevel;
        
        public ArcadeMenuUi( GameObject gameObject )
        {
            var childContainer = new GameObjectChildrenContainer( gameObject );

            Level = new LevelUi( childContainer.Get( "level" ) );

            _menuContainer = new GameObjectChildrenContainer( childContainer.Get( "menu_container" ) );
            _menuContainer.Get( "back_button" ).GetComponent<Button>().onClick.AddListener( GetBackButtonEventManager.Trigger );
            LevelsContainer = _menuContainer.Get( "levels" ); 
            ExampleLevel = LevelsContainer.FindChild( "example_level" );
        }

        public void SetActive( bool activity )
        {
            _menuContainer.GameObject.SetActive( activity );

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