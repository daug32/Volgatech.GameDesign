using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Application.Menus.Common.Books.Elements.Handlers;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Common.Books
{
    internal class Book
    {
        public readonly RectTransform RectTransform;
        public readonly HashSet<ElementId> DiscoveredElements = new();

        public readonly GameObject InteractiveElementsContainer;
        public readonly GameObject BookElementsContainer;

        public readonly EventManager<ElementId> OnElementCreationSuccessEvent = new();
        public readonly EventManager<ElementId> OnElementCreationFailureEvent = new();
        
        public Book( GameObject gameObject )
        {
            RectTransform = gameObject.GetComponent<RectTransform>();

            var childManager = new GameObjectChildrenContainer( gameObject );
            InteractiveElementsContainer = childManager.Get( "interactive_elements_container" );
            BookElementsContainer = childManager.Get( "book_elements_container" );

            OnElementCreationFailureEvent.AddWithHighestPriority( _ => SoundSourceBehaviour.Instance.PlaySound( SoundType.ElementCreationFailed ) );
            OnElementCreationSuccessEvent.AddWithHighestPriority( _ => SoundSourceBehaviour.Instance.PlaySound( SoundType.ElementCreationSuccess ) );
            OnElementCreationSuccessEvent.AddWithHighestPriority( Draw );
        }

        public void Load( IEnumerable<ElementId> starterElements )
        {
            Unload();
            
            DiscoveredElements.Clear();
            DiscoveredElements.AddRange( starterElements );
            var orderedElements = DiscoveredElements
               .Select( x => new
                {
                    ElementId = x,
                    Data = ElementsDataRepository.Get( x ),
                    Element = ElementsRepository.Get( x ),
                } )
               .OrderBy( x => x.Data.DisplayOrder )
               .Select( x => x.Element )
               .ToList();

            var elements = new List<GameObject>();
            foreach ( var element in orderedElements )
            {
                GameObject elementGameObject = element
                   .CreateGameObject( false )
                   .WithParent( BookElementsContainer );
                elementGameObject.AddIconDragAndDrop( element, this );
                elements.Add( elementGameObject );
            }
            
            // Used to show all elements at once
            elements.ForEach( x => x.SetActive( true ) );
        }

        public void Unload()
        {
            foreach ( Transform element in InteractiveElementsContainer.transform )
            {
                Object.Destroy( element.gameObject ); 
            }

            foreach ( var gameObject in BookElementsContainer.FindChildren() )
            {
                Object.Destroy( gameObject );
            }
        }

        private void Draw( ElementId elementId )
        {
            if ( DiscoveredElements.Contains( elementId ) )
            {
                return;
            }

            DiscoveredElements.Add( elementId );
            Element element = ElementsRepository.Get( elementId );
            GameObject elementGameObject = element.CreateGameObject().WithParent( BookElementsContainer );
            elementGameObject.AddIconDragAndDrop( element, this );
        }
    }
}