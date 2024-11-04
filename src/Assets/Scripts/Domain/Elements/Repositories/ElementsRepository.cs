using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Domain.Elements.Repositories
{
    internal static class ElementsRepository
    {
        private static readonly string _elementsFolder = Config.ElementsIconsFolder;
        private static readonly Dictionary<ElementId, Element> _elements;

        static ElementsRepository()
        {
            _elements = Resources.LoadAll<Sprite>( _elementsFolder )
                                 .Select( sprite => new Element( sprite ) )
                                 .ToDictionary( element => element.Id, element => element );
            if ( !_elements.Any() )
            {
                throw new ArgumentException( $"Failed to load elements assets. Search folders: [{String.Join( ", ", _elementsFolder )}]" );
            }
        }

        public static Element Get( ElementId id ) => _elements[ id ];
        public static bool Exists( ElementId id ) => _elements.ContainsKey( id );
        public static List<Element> GetAll() => _elements.Values.ToList();
    }
}
