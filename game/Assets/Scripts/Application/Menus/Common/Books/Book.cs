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
        
        public Book( GameObject gameObject )
        {
            GameObject = gameObject.ThrowIfNull( nameof( Book ) );
            RectTransform = gameObject.GetComponent<RectTransform>();
        }

        public void DrawAll()
        {
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
                elementGameObject.AddIconDragAndDrop( element, RectTransform );
                elements.Add( elementGameObject );
            }
            
            // Used to show all elements at once
            elements.ForEach( x => x.SetActive( true ) );
        }

        public void Draw( ElementId elementId )
        {
            Element element = ElementsRepository.Get( elementId );            
            GameObject elementGameObject = element.CreateGameObject().WithParent( GameObject );
            elementGameObject.AddIconDragAndDrop( element, RectTransform );
        }

        public void RemoveAll()
        {
            foreach ( Transform element in GameObject.transform )
            {
                Object.Destroy( element.gameObject );
            }
        }
    }
}