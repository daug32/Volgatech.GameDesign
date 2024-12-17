using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal static class GameObjectExtensions
    {
        public static GameObject WithParent( this GameObject item, GameObject parent )
        {
            Vector3 scale = item.transform.localScale;
            item.transform.SetParent( parent.transform );
            item.transform.localScale = scale;
            return item;
        }

        public static GameObject FindChild( this GameObject gameObject, string childName ) =>
            FindChildren( gameObject )
               .FirstOrDefault( x => x.name == childName );

        public static IEnumerable<GameObject> FindChildren( this GameObject gameObject )
        {
            foreach ( object childObject in gameObject.transform )
            {
                if ( childObject == null || childObject is not Transform childTransform )
                {
                    continue;
                }

                yield return childTransform.gameObject;
            }
        }

        public static GameObject WithSize( this GameObject gameObject, Vector2 size )
        {
            var rectTransform = gameObject.GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = size;
            return gameObject;
        }
    }
}
