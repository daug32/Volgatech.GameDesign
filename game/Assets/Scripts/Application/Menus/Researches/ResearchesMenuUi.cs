using System.Collections.Generic;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Researches
{
    public class ResearchesMenuUi
    {
        private readonly GameObject _gameObject;

        private readonly List<ElementId> _drawnElements;

        private readonly GameObject _elementsContainer;

        public readonly EventManager OnOpenMainMenuEvent = new();

        public ResearchesMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );

            var childManager = new GameObjectChildrenContainer( gameObject );
            _elementsContainer = childManager.Get( "elements_container" );
            OnOpenMainMenuEvent.SubscribeOnClick( childManager.Get( "settings_button" ) );
        }

        public void SetActive( bool isActive )
        {
            if ( isActive )
            {
                DrawDiscoveredElements();
            }
            
            _gameObject.SetActive( isActive );
        }

        private void DrawDiscoveredElements()
        {
        }
    }
}