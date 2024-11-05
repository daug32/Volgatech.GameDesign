using System;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Domain.Ui
{
    internal static class UiItemRepository
    {
        private static readonly Lazy<GameObject> _canvas = new( () => GameObject
            .Find( "UI" )
            .ThrowIfNull( message: "Failed to find a canvas at the scene. Expected element name: UI" ) ); 
        public static GameObject GetCanvas() => _canvas.Value;

        private static readonly Lazy<GameObject> _book = new( () => GameObject
           .Find( "book" )
           .ThrowIfNull( message: "Failed to find a book at the scene. Expected element name: book" ) );
        public static GameObject GetBook() => _book.Value;
        
        private static readonly Lazy<GameObject> _targets = new( () => GameObject
            .Find( "targets" )
            .ThrowIfNull( message: "Failed to find targets layout at the scene. Expected element name: targets" ) );
        public static GameObject GetTargets() => _targets.Value;

        private static readonly Lazy<GameObject> _levelObject = new( () => GameObject
            .Find( "level" )
            .ThrowIfNull( message: "Failed to find level object at the scene. Expected element name: level" ) );
        public static GameObject GetLevelObject() => _levelObject.Value;
    }
}
