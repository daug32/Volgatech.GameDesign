using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
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
        public readonly EventManager OnSoundButtonPressedEvent = new();

        public MainMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );

            var mainMenuChildrenContainer = new GameObjectChildrenContainer( gameObject );
            OnSoundButtonPressedEvent
               .SubscribeOnClick( mainMenuChildrenContainer.Get( "sound_button" ) )
               .AddWithLowestPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
            
            var mainListChildrenContainer = new GameObjectChildrenContainer( mainMenuChildrenContainer.Get( "main_list" ) );
            OnOpenArcadeEvent
               .SubscribeOnClick( mainListChildrenContainer.Get( "arcade_button" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
            OnOpenSandboxEvent
               .SubscribeOnClick( mainListChildrenContainer.Get( "sandbox_button" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
            OnOpenResearchesMenuEvent
               .SubscribeOnClick( mainListChildrenContainer.Get( "tree_button" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
            mainListChildrenContainer.Get( "exit_button" ).GetComponent<Button>().onClick.AddListener( UnityEngine.Application.Quit );
        }

        public void SetActive( bool activity ) => _gameObject.SetActive( activity );
    }
}