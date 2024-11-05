using Assets.Scripts.Domain.Elements.Handlers;
using Assets.Scripts.Repositories;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Domain.Elements
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

            DndBehaviour = gameObject.AddInteractiveElementDragAndDrop(
                element,
                UiItemRepository.GetBook().GetComponent<RectTransform>(),
                SceneId );
            CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public static InteractiveElement Create( 
            Element element, 
            Vector2 sizeDelta )
        {
            return new InteractiveElement(
                element,
                sizeDelta,
                element.CreateGameObject().WithParent( UiItemRepository.GetCanvas() ) );
        }
    }
}