using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Assets.Scripts.Domain.Elements.Repositories
{
    internal static class ElementsRepository
    {
        private static readonly string[] _elementsFolders = new[] { Config.ElementsIconsFolder };
        private static readonly Dictionary<ElementId, Element> _elements;

        static ElementsRepository()
        {
            _elements = AssetDatabase
                .FindAssets( "", _elementsFolders )
                .Select( BuildElement )
                .ToDictionary( element => element.Id, element => element );
            if ( !_elements.Any() )
            {
                throw new ArgumentException( $"Failed to load elements assets. Search folders: [{String.Join( ", ", _elementsFolders )}]" );
            }
        }

        public static Element Get( ElementId id ) => _elements[ id ];
        public static List<Element> GetAll() => _elements.Values.ToList();

        private static Element BuildElement( string guid )
        {
            return new Element( 
                guid,
                AssetDatabase.GUIDToAssetPath( guid ) );
        }
    }
}
