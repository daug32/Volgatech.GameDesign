using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Elements.Handlers;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui.Books.Handlers
{
    internal class DrawBookElementsHandler
    {
        public void DrawAll()
        {
            var book = UiItemsRepository.GetBook();
            
            foreach ( Element element in ElementsRepository.GetAll() )
            {
                if ( !ElementsDataRepository.Get( element.Id ).IsDiscovered )
                {
                    continue;
                }

                GameObject elementGameObject = element.CreateGameObject().WithParent( book.GameObject );
                elementGameObject.AddIconDragAndDrop( element, book.RectTransform );
            }
        }

        public void Draw( ElementId elementId )
        {
            var book = UiItemsRepository.GetBook();

            Element element = ElementsRepository.Get( elementId );            
            GameObject elementGameObject = element.CreateGameObject().WithParent( book.GameObject );
            elementGameObject.AddIconDragAndDrop( element, book.RectTransform );
        }

        public void RemoveAllElements()
        {
            var book = UiItemsRepository.GetBook();
            foreach ( Transform element in book.GameObject.transform )
            {
                Object.Destroy( element.gameObject );
            }
        }
    }
}