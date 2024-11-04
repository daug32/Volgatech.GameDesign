using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Domain.Elements.Repositories
{
    internal static class InteractiveElementRepository
    {
        private static readonly Dictionary<Guid, InteractiveElement> _elements = new();

        public static void Add( InteractiveElement element ) => _elements.Add( element.SceneId, element );

        public static InteractiveElement Get( Guid id )
        {
            if ( !_elements.TryGetValue( id, out InteractiveElement element ) )
            {
                throw new ArgumentException( $"Failed to get interactive element. Id: {id}" );
            }
            return element; 
        }

        public static bool Exists( Guid id ) => _elements.ContainsKey( id );

        public static void Remove( Guid id )
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