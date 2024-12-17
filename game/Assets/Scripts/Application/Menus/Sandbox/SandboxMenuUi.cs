using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Application.Menus.Common.Books;
using Assets.Scripts.Application.Users.Repositories;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Sandbox
{
    internal class SandboxMenuUi
    {
        private readonly GameObject _gameObject;

        private readonly Book _book;

        public readonly EventManager OnOpenMainMenuEvent = new();
        
        public SandboxMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );

            var childManager = new GameObjectChildrenContainer( gameObject );
            _book = new Book( childManager.Get( "book" ) );
            _book.OnElementCreationSuccessEvent.AddWithCommonPriority( elementId => UserDataRepository.Get().DiscoveredElements.Add( elementId ) );
            OnOpenMainMenuEvent
               .SubscribeOnClick( childManager.Get( "settings_button" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
        }

        public void SetActive( bool isActive )
        {
            _gameObject.SetActive( isActive );

            if ( isActive )
            {
                ElementsInteractionBlocker.AllowInteractions();
                _book.Load( UserDataRepository.Get().DiscoveredElements );
            }
            else
            {
                _book.Unload();
            }
        }
    }
}