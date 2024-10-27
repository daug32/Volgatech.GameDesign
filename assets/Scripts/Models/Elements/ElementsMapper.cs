using Assets.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Elements
{
    internal static class ElementsMapper
    {
        public static GameObject MapToGameObject( this Element element )
        {
            var elementGameObject = new GameObject();

            Image elementUiImage = elementGameObject.AddComponent<Image>();
            elementGameObject.name = $"element_{element.Id}";
            elementUiImage.preserveAspect = true;
            elementUiImage.sprite = element.Sprite;
            elementGameObject.SetActive( true );

            return elementGameObject;
        }
    }
}
