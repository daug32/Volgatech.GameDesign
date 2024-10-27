using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models.Levels;
using Assets.Scripts.Repositories.Elements.ElementsData;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Models.Elements
{
    internal class ElementsDataRepository
    {
        private readonly Dictionary<ElementId, ElementData> _data;

        public ElementData Get( ElementId id ) => _data.ContainsKey( id ) ? _data[ id ] : new ElementData( Array.Empty<string>() );

        public ElementsDataRepository( LevelType levelType )
        {
            string jsonData = JsonHelper.MergeJsons(
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/default.json" ),
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/{levelType.ToDatabaseFilename()}" ) );
            _data = LoadData( jsonData ).ToDictionary( x => x.Key, x => x.Value );
        }

        private Dictionary<ElementId, ElementData> LoadData( string json )
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
    