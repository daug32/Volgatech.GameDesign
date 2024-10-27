using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal static class GameObjectExtensions
    {
        public static GameObject WithParent( this GameObject item, GameObject parent )
        {
            Vector3 scale = item.transform.localScale;
            item.transform.parent = parent.transform;
            item.transform.localScale = scale;
            return item;
        }
    }
}
