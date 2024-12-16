using Assets.Scripts.Application.Menus.Common.Books;
using Assets.Scripts.Application.Menus.Common.Books.Handlers;
using Assets.Scripts.Application.Users.Repositories;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Sandbox
{
    internal class SandboxMenuUi
    {
        private readonly GameObject _gameObject;

        private readonly Book _book;

        public readonly EventManager OnOpenMainMenuEvent = new();
        
        public SandboxMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( message: "No game object provided" );

            var childManager = new GameObjectChildrenContainer( gameObject );
            _book = new Book( childManager.Get( "book" ) );
            _book.OnElementCreatedEvent.AddWithCommonPriority( elementId => UserDataRepository.Get().DiscoveredElements.Add( elementId ) );
            childManager.Get( "settings_button" ).GetComponent<Button>().onClick.AddListener( OnOpenMainMenuEvent.Trigger );
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