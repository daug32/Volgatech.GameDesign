using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Levels;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Domain.Elements.Repositories.ElementsData
{
    internal static class ElementsDataRepository
    {
        private static Dictionary<ElementId, ElementData> _data;

        public static void LoadForLevel( LevelType levelType )
        {
            string jsonData = JsonHelper.MergeJsons(
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/default" ),
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/{levelType.ToDatabaseFilename()}" ) );
            _data = LoadData( jsonData ).ToDictionary( x => x.Key, x => x.Value );
        }

        public static ElementData Get( ElementId id ) => _data.ContainsKey( id ) ? _data[ id ] : new ElementData( Array.Empty<string>() );
        public static List<ElementId> GetAll() => _data.Keys.ToList();
        public static bool Exists( ElementId id ) => _data.ContainsKey( id );
        public static ElementId GetByParents( ElementId firstParent, ElementId secondParent )
        {
            var keyValuePair = _data.FirstOrDefault( 
                x => x.Value.Parents.Contains( firstParent ) && x.Value.Parents.Contains( secondParent ) );
            return keyValuePair.Key;
        }

        private static Dictionary<ElementId, ElementData> LoadData( string json )
        {
            ElementDataContainerDto dataContainer = JsonHelper.Deserialize<ElementDataContainerDto>( json );
            if ( dataContainer == null )
            {
                throw new ArgumentException( $"Failed to load data. Asset: {json}" );
            }

            var result = new Dictionary<ElementId, ElementData>();
            foreach ( ( string elementId, ElementDataDto dataDto ) in dataContainer )
            {
                result.Add( new ElementId( elementId ), dataDto.Convert() );
            }

            return result;
        }
    }
}
    