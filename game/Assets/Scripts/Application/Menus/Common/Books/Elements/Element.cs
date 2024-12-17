using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements
{
    internal class Element
    {
        public readonly string Guid;

        public readonly ElementId Id;
        public readonly string AssetsPath;

        public readonly Sprite Sprite;

        public Element( Sprite sprite )
        {
            Id = new ElementId( sprite.name );
            Sprite = sprite;
        }

        public override string ToString() => $"Element (id: ${Guid}, path: ${AssetsPath})";

        public string BuildName() => $"element_{Id}";

        public GameObject CreateGameObject( bool setActive = true )
        {
            var elementGameObject = new GameObject();

            var elementUiImage = elementGameObject.AddComponent<Image>();
            elementGameObject.name = BuildName();
            elementUiImage.preserveAspect = true;
            elementUiImage.sprite = Sprite;

            if ( setActive )
            {
                elementGameObject.SetActive( true );
            }

            return elementGameObject;
        }
    }
}
