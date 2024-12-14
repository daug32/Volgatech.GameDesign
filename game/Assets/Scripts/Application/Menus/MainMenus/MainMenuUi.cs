using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.MainMenus
{
    internal class MainMenuUi
    {
        private readonly GameObject _gameObject;

        public readonly EventManager SoundButtonEventManager = new();
        public readonly EventManager ArcadeButtonEventManager = new();
        public readonly EventManager SandboxButtonEventManager = new();
        public readonly EventManager TreeButtonEventManager = new();
        public readonly EventManager ExitButtonEventManager = new();

        public MainMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );
            var mainMenuChildrenContainer = new GameObjectChildrenContainer( gameObject );
            mainMenuChildrenContainer.Get( "sound_button" ).GetComponent<Button>().onClick.AddListener( SoundButtonEventManager.Trigger );
            
            var mainListChildrenContainer = new GameObjectChildrenContainer( mainMenuChildrenContainer.Get( "main_list" ) );
            mainListChildrenContainer.Get( "arcade_button" ).GetComponent<Button>().onClick.AddListener( ArcadeButtonEventManager.Trigger );
            mainListChildrenContainer.Get( "sandbox_button" ).GetComponent<Button>().onClick.AddListener( SandboxButtonEventManager.Trigger );
            mainListChildrenContainer.Get( "tree_button" ).GetComponent<Button>().onClick.AddListener( TreeButtonEventManager.Trigger );
            mainListChildrenContainer.Get( "exit_button" ).GetComponent<Button>().onClick.AddListener( ExitButtonEventManager.Trigger );
        }

        public void SetActive( bool activity ) => _gameObject.SetActive( activity );
    }
}