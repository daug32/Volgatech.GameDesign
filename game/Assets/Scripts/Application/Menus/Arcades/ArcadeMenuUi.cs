using Assets.Scripts.Application.Menus.Arcades.Handlers;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Ui.Arcades
{
    internal class ArcadeMenuUi
    {
        private readonly GameObject _gameObject;
        public readonly EventManager GetBackButtonEventManager = new();
        public readonly EventManager<LevelType> ChooseLevelEventManger = new();
        
        public readonly GameObject LevelsContainer;
        public readonly GameObject ExampleLevel;
        
        public ArcadeMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            
            var childContainer = new GameObjectChildrenContainer( gameObject );
            childContainer.Get( "back_button" ).GetComponent<Button>().onClick.AddListener( GetBackButtonEventManager.Trigger );

            LevelsContainer = childContainer.Get( "levels" ); 
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