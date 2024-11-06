using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Ui
{
    internal class MainMenuUi
    {
        private readonly GameObject _gameObject;
        
        public readonly EventManager ArcadeButtonEventManager = new();
        public readonly EventManager ExitButtonEventManager = new();

        public MainMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            
            var childrenContainer = new GameObjectChildrenContainer( gameObject );
            childrenContainer.Get( "arcade_button" ).GetComponent<Button>().onClick.AddListener( ArcadeButtonEventManager.Trigger );
            childrenContainer.Get( "exit_button" ).GetComponent<Button>().onClick.AddListener( ExitButtonEventManager.Trigger );
        }

        public void SetActive( bool activity ) => _gameObject.SetActive( activity );
    }
}