using System.Collections.Generic;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Elements.Handlers;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui.Books.Handlers
{
    internal static class DrawBookElementsHandler
    {
        public static void DrawAll()
        {
            var book = UiItemsRepository.GetUserInterface().Level.Book;

            var elements = new List<GameObject>();
            foreach ( Element element in ElementsRepository.GetAll() )
            {
                if ( !ElementsDataRepository.Get( element.Id ).IsDiscovered )
                {
                    continue;
                }

                GameObject elementGameObject = element
                   .CreateGameObject( false )
                   .WithParent( book.GameObject );
                elementGameObject.AddIconDragAndDrop( element, book.RectTransform );
                elements.Add( elementGameObject );
            }
            
            // Used to show all elements at once
            elements.ForEach( x => x.SetActive( true ) );
        }

        public static void Draw( ElementId elementId )
        {
            var book = UiItemsRepository.GetUserInterface().Level.Book;

            Element element = ElementsRepository.Get( elementId );            
            GameObject elementGameObject = element.CreateGameObject().WithParent( book.GameObject );
            elementGameObject.AddIconDragAndDrop( element, book.RectTransform );
        }

        public static void RemoveAllElements()
        {
            var book = UiItemsRepository.GetUserInterface().Level.Book;
            
            foreach ( Transform element in book.GameObject.transform )
            {
                Object.Destroy( element.gameObject );
            }
        }
    }
}