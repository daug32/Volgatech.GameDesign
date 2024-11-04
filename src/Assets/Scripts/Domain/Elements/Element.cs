using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Domain.Elements
{
    internal class Element
    {
        public readonly string Guid;

        public readonly ElementId Id;
        public readonly string AssetsPath;

        private readonly Lazy<Sprite> _sprite;

        public Element( string guid, string path )
        {
            Guid = guid;
            AssetsPath = path;

            string[] nameParts = Path.GetFileNameWithoutExtension( AssetsPath ).Split( '_' );
            Id = new ElementId( nameParts.Last() );

            _sprite = new( AssetDatabase.LoadAssetAtPath<Sprite>( AssetsPath ) );
        }

        public override string ToString() => $"Element (id: ${Guid}, path: ${AssetsPath})";

        public GameObject CreateGameObject()
        {
            var elementGameObject = new GameObject();

            var elementUiImage = elementGameObject.AddComponent<Image>();
            elementGameObject.name = $"element_{Id}";
            elementUiImage.preserveAspect = true;
            elementUiImage.sprite = _sprite.Value;
            elementGameObject.SetActive( true );

            return elementGameObject;
        }
    }
}
