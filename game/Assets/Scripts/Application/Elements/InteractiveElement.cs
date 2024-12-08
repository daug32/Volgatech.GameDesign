using Assets.Scripts.Application.Elements.Handlers;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Elements
{
    internal class InteractiveElement
    {
        public readonly InteractiveElementId SceneId = new();

        public readonly Element Element;
        public readonly GameObject GameObject;

        public readonly RectTransform RectTransform;
        public readonly ElementDndBehaviour DndBehaviour;
        public readonly CanvasGroup CanvasGroup;

        private InteractiveElement( 
            Element element,
            Vector2 sizeDelta,
            GameObject gameObject )
        {
            Element = element;

            GameObject = gameObject;
            GameObject.name = $"{element.BuildName()}_{SceneId}";
            
            RectTransform = gameObject.GetComponent<RectTransform>();
            RectTransform.sizeDelta = sizeDelta;

            var book = UiItemsRepository.GetUserInterface().Level.Book;
            DndBehaviour = gameObject.AddInteractiveElementDragAndDrop(
                element,
                book.RectTransform,
                SceneId );
            CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public static InteractiveElement Create( 
            Element element, 
            Vector2 sizeDelta )
        {
            var userInterface = UiItemsRepository.GetUserInterface();
            
            return new InteractiveElement(
                element,
                sizeDelta,
                element.CreateGameObject().WithParent( userInterface.Level.InteractiveElementsContainer ) );
        }
    }
}