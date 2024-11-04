using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Domain.Elements
{
    internal class Element
    {
        public readonly string Guid;

        public readonly ElementId Id;
        public readonly string Name;
        public readonly string AssetsPath;

        private readonly Lazy<Sprite> _sprite;
        public Sprite Sprite => _sprite.Value;

        public Element( string guid, string path )
        {
            Guid = guid;
            AssetsPath = path;

            string[] nameParts = Path.GetFileNameWithoutExtension( AssetsPath ).Split( '_' );
            Id = new ElementId( nameParts.Last() );

            _sprite = new Lazy<Sprite>( () => AssetDatabase.LoadAssetAtPath<Sprite>( AssetsPath )  );
        }

        public override string ToString() => $"Element (id: ${Guid}, path: ${AssetsPath})";
    }
}
