using System.Collections.Generic;
using Assets.Scripts.Domain.Elements.Repositories;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;
using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    internal class DrawElementsHandler
    {
        private readonly HashSet<ElementId> _drawnElements = new();
        
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
                _drawnElements.Add( element.Id );
            }
        }

        public void Draw( ElementId elementId )
        {
            if ( _drawnElements.Contains( elementId ) )
            {
                return;
            }
            
            var book = UiItemRepository.GetBook();
            var bookRectTransform = book.GetComponent<RectTransform>();

            Element element = ElementsRepository.Get( elementId );            
            GameObject elementGameObject = element.CreateGameObject().WithParent( book );
            elementGameObject.AddIconDragAndDrop( element, bookRectTransform );

            _drawnElements.Add( elementId );
        }
    }
}