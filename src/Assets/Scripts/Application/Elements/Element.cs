using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Elements
{
    internal class Element
    {
        public readonly string Guid;

        public readonly ElementId Id;
        public readonly string AssetsPath;

        private readonly Sprite _sprite;

        public Element( Sprite sprite )
        {
            string[] nameParts = sprite.name.Split( '_' );
            Id = new ElementId( nameParts.Last() );

            _sprite = sprite;
        }

        public override string ToString() => $"Element (id: ${Guid}, path: ${AssetsPath})";

        public string BuildName() => $"element_{Id}";

        public GameObject CreateGameObject( bool setActive = true )
        {
            var elementGameObject = new GameObject();

            var elementUiImage = elementGameObject.AddComponent<Image>();
            elementGameObject.name = BuildName();
            elementUiImage.preserveAspect = true;
            elementUiImage.sprite = _sprite;

            if ( setActive )
            {
                elementGameObject.SetActive( true );
            }

            return elementGameObject;
        }
    }
}
