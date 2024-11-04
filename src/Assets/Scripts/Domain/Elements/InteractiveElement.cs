using System;
using Assets.Scripts.Domain.Elements.Handlers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Domain.Elements
{
    internal class InteractiveElement
    {
        public readonly Guid SceneId = Guid.NewGuid();

        public readonly Element Element;
        public readonly GameObject GameObject;

        public readonly RectTransform RectTraTransform;
        public readonly ElementDnDBehaviour DnDBehaviour;
        public readonly CanvasGroup CanvasGroup;

        private InteractiveElement( 
            Element element,
            GameObject gameObject )
        {
            Element = element;
            GameObject = gameObject;

            RectTraTransform = gameObject.GetComponent<RectTransform>();
            DnDBehaviour = gameObject.AddComponent<ElementDnDBehaviour>();
            CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public static InteractiveElement Create( 
            Element element, 
            GameObject elementIcon,
            GameObject parent )
        {
            var interactiveElement = new InteractiveElement(
                element,
                element.CreateGameObject().WithParent( parent ) );
            
            RectTransform iconRectTransform = elementIcon.GetComponent<RectTransform>();
            interactiveElement.RectTraTransform.sizeDelta = iconRectTransform.sizeDelta;

            return interactiveElement;
        }
    }
}