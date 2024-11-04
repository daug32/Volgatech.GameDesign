using Assets.Scripts.Common.Behaviours;
using UnityEngine;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    internal static class DragAndDropBehaviourApplier
    {
        public static DragAndDropBehaviour AddDragAndDrop( this GameObject obj, Canvas canvas )
        {
            var handler = obj.AddComponent<DragAndDropBehaviour>();
            handler.Canvas = canvas;

            return handler;
        }
    }
}