using System;
using System.Collections.Generic;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Application.Menus.Common.Books.Repositories
{
    internal static class InteractiveElementRepository
    {
        private static readonly Dictionary<InteractiveElementId, InteractiveElement> _elements = new();

        public static void Add( InteractiveElement element )
        {
            _elements.Add( element.Id, element );
        }

        public static InteractiveElement Get( InteractiveElementId id )
        {
            if ( !_elements.TryGetValue( id, out var element ) )
            {
                throw new ArgumentException( $"Failed to get interactive element. Id: {id}" );
            }

            return element;
        }

        public static bool Exists( InteractiveElementId id )
        {
            return _elements.ContainsKey( id );
        }

        public static void RemoveAll()
        {
            foreach ( (_, InteractiveElement element) in _elements )
            {
                Object.Destroy( element.GameObject );
            }

            _elements.Clear();
        }

        public static void Remove( InteractiveElementId id )
        {
            if ( Exists( id ) )
            {
                var element = Get( id );
                _elements.Remove( id );
                Object.Destroy( element.GameObject );
            }
        }
    }
}