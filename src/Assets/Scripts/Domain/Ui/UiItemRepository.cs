using System;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Domain.Ui
{
    internal static class UiItemRepository
    {
        private static readonly Lazy<Canvas> _canvas = new( () => GameObject
            .Find( "UI" )
            .ThrowIfNull( message: "Failed to find a canvas at the scene. Expected element name: UI" )
            .GetComponent<Canvas>()
            .ThrowIfNull( message: "Failed to get canvas component from game object. GameObjectName: UI" ) ); 
        public static Canvas GetCanvas() => _canvas.Value;

        private static readonly Lazy<GameObject> _book = new( () => GameObject
           .Find( "book" )
           .ThrowIfNull( message: "Failed to find a book at the scene. Expected element name: book" ) );
        public static GameObject GetBook() => _book.Value;
    }
}
