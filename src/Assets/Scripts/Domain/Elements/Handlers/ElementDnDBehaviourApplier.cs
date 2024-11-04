using UnityEngine;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    internal static class ElementDnDBehaviourApplier
    {
        public static ElementDnDBehaviour AddDragAndDrop( this GameObject obj, RectTransform bookRectTransform )
        {
            var dnd = obj.AddComponent<ElementDnDBehaviour>();
            dnd.BookRectTransform = bookRectTransform;

            var group = obj.AddComponent<CanvasGroup>();
            group.interactable = true;

            return dnd;
        } 
    }
}