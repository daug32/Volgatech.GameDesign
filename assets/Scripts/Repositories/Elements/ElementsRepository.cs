using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Models.Elements;
using UnityEditor;

namespace Assets.Settings
{
    internal static class ElementsRepository
    {
        private static string[] _elementsFolders = new[] { Config.ElementsIconsFolder };

        public static Dictionary<ElementId, Element> Elements;

        static ElementsRepository()
        {
            Elements = AssetDatabase
                .FindAssets( "", _elementsFolders )
                .Select( guid => BuildElement( guid ) )
                .ToDictionary( element => element.Id, element => element );
            if ( !Elements.Any() )
            {
                throw new ArgumentException( $"Failed to load elements assets. Search folders: [{String.Join( ", ", _elementsFolders )}]" );
            }
        }

        private static Element BuildElement( string guid )
        {
            return new Element( 
                guid,
                AssetDatabase.GUIDToAssetPath( guid ) );
        }
    }
}
