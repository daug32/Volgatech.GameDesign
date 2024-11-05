using Assets.Scripts.Domain.Elements;
using Assets.Scripts.Domain.Elements.Handlers;
using Assets.Scripts.Repositories;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Domain.Book.Handlers
{
    internal class DrawBookElementsHandler
    {
        public void DrawAll()
        {
            var book = UiItemRepository.GetBook();
            var bookRectTransform = book.GetComponent<RectTransform>();
            
            foreach ( Element element in ElementsRepository.GetAll() )
            {
                if ( !ElementsDataRepository.Get( element.Id ).IsDiscovered )
                {
                    continue;
                }

                GameObject elementGameObject = element.CreateGameObject().WithParent( book );
                elementGameObject.AddIconDragAndDrop( element, bookRectTransform );
            }
        }

        public void Draw( ElementId elementId )
        {
            var book = UiItemRepository.GetBook();
            var bookRectTransform = book.GetComponent<RectTransform>();

            Element element = ElementsRepository.Get( elementId );            
            GameObject elementGameObject = element.CreateGameObject().WithParent( book );
            elementGameObject.AddIconDragAndDrop( element, bookRectTransform );
        }

        public void RemoveAllElements()
        {
            var book = UiItemRepository.GetBook();
            foreach ( Transform element in book.transform )
            {
                Object.Destroy( element.gameObject );
            }
        }
    }
}