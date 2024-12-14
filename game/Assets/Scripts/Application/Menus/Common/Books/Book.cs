using System.Collections.Generic;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Application.Menus.Common.Books.Elements.Handlers;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Common.Books
{
    internal class Book
    {
        public readonly GameObject GameObject;
        public readonly RectTransform RectTransform;

        public readonly EventManager<ElementId> OnElementCreated = new();

        public readonly GameObject InteractiveElementsContainer;
        
        public Book( GameObject gameObject )
        {
            GameObject = gameObject.ThrowIfNull( nameof( Book ) );
            RectTransform = gameObject.GetComponent<RectTransform>();
            InteractiveElementsContainer = GameObject
               .FindChild( "interactive_elements_container" )
               .ThrowIfNull( nameof( InteractiveElementsContainer ) );
            OnElementCreated.AddWithHighestPriority( Draw );
        }

        public void Load()
        {
            Unload();

            var elements = new List<GameObject>();
            foreach ( Element element in ElementsRepository.GetAll() )
            {
                if ( !ElementsDataRepository.Get( element.Id ).IsDiscovered )
                {
                    continue;
                }

                GameObject elementGameObject = element
                   .CreateGameObject( false )
                   .WithParent( GameObject );
                elementGameObject.AddIconDragAndDrop( element, this );
                elements.Add( elementGameObject );
            }
            
            // Used to show all elements at once
            elements.ForEach( x => x.SetActive( true ) );
        }

        public void Draw( ElementId elementId )
        {
            var elementData = ElementsDataRepository.Get( elementId );
            if ( elementData.IsDiscovered )
            {
                return;
            }

            Element element = ElementsRepository.Get( elementId );
            elementData.IsDiscovered = true;
                  
            GameObject elementGameObject = element.CreateGameObject().WithParent( GameObject );
            elementGameObject.AddIconDragAndDrop( element, this );
        }

        public void Unload()
        {
            foreach ( Transform element in InteractiveElementsContainer.transform )
            {
                Object.Destroy( element.gameObject ); 
            }

            foreach ( var gameObject in GameObject.FindChildren() )
            {
                if ( gameObject == InteractiveElementsContainer )
                {
                    continue;
                }
                
                Object.Destroy( gameObject );
            }
        }
    }
}