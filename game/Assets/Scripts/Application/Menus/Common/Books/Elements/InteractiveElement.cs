using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Application.Menus.Common.Books.Elements.Handlers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements
{
    internal class InteractiveElement
    {
        public readonly InteractiveElementId Id = new();

        public readonly Element Element;
        public readonly GameObject GameObject;

        public readonly RectTransform RectTransform;
        public readonly ElementDndBehaviour DndBehaviour;
        public readonly CanvasGroup CanvasGroup;

        private InteractiveElement( 
            Element element,
            Vector2 sizeDelta,
            Book book )
        {
            Element = element;

            GameObject = element.CreateGameObject().WithParent( book.InteractiveElementsContainer );
            GameObject.name = $"{element.BuildName()}_{Id}";
            
            RectTransform = GameObject.GetComponent<RectTransform>();
            RectTransform.sizeDelta = sizeDelta;

            DndBehaviour = GameObject.AddInteractiveElementDragAndDrop( element, Id, book );
            CanvasGroup = GameObject.AddComponent<CanvasGroup>();
        }

        public static InteractiveElement Create( 
            Element element, 
            Vector2 sizeDelta,
            Book book )
        {
            return new InteractiveElement(
                element,
                sizeDelta,
                book );
        }
    }
}