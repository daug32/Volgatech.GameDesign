using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.MainMenus
{
    internal class MainMenuUi
    {
        private readonly GameObject _gameObject;

        public readonly EventManager OnOpenArcadeEvent = new();
        public readonly EventManager OnOpenSandboxEvent = new();
        public readonly EventManager OnOpenResearchesMenuEvent = new();
        public readonly EventManager OnExitEvent = new();
        public readonly EventManager OnSoundButtonPressedEvent = new();

        public MainMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );
            var mainMenuChildrenContainer = new GameObjectChildrenContainer( gameObject );
            mainMenuChildrenContainer.Get( "sound_button" ).GetComponent<Button>().onClick.AddListener( OnSoundButtonPressedEvent.Trigger );
            
            var mainListChildrenContainer = new GameObjectChildrenContainer( mainMenuChildrenContainer.Get( "main_list" ) );
            mainListChildrenContainer.Get( "arcade_button" ).GetComponent<Button>().onClick.AddListener( OnOpenArcadeEvent.Trigger );
            mainListChildrenContainer.Get( "sandbox_button" ).GetComponent<Button>().onClick.AddListener( OnOpenSandboxEvent.Trigger );
            mainListChildrenContainer.Get( "tree_button" ).GetComponent<Button>().onClick.AddListener( OnOpenResearchesMenuEvent.Trigger );
            mainListChildrenContainer.Get( "exit_button" ).GetComponent<Button>().onClick.AddListener( OnExitEvent.Trigger );
        }

        public void SetActive( bool activity ) => _gameObject.SetActive( activity );
    }
}