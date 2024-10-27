using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Models.Elements
{
    internal class ElementsDataRepository
    {
        private readonly Dictionary<ElementId, ElementData> _data;

        public ElementData Get( ElementId id ) => _data.ContainsKey( id ) ? _data[ id ] : new ElementData( Array.Empty<string>() );

        public void Save( ElementId id, ElementData data ) => _data[ id ] = data;

        public ElementsDataRepository()
        {
            _data = LoadData( $"{Config.ElementsDataDatabase}/default.json" ).ToDictionary( 
                x => x.Key, 
                x => x.Value.Convert() );
        }

        private Dictionary<ElementId, ElementDataDto> LoadData( string assetPath )
        {
            TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>( assetPath );
            if ( asset == null )
            {
                throw new ArgumentException( $"Failed to load data. Asset path: {assetPath}" );
            }

            List<ElementDataContainerDto> dataContainer = JsonSerializer.Deserialize<List<ElementDataContainerDto>>( asset.text );
            if ( dataContainer == null )
            {
                throw new ArgumentException( $"Failed to load data. Asset path: {assetPath}" );
            }

            var result = new Dictionary<ElementId, ElementDataDto>();
            foreach ( ElementDataContainerDto dataDto in dataContainer )
            {
                result.Add(
                    new ElementId( dataDto.Id.ThrowIfNull( $"{nameof( ElementDataContainerDto )}.{nameof( ElementDataContainerDto.Id )}" ) ),
                    dataDto.Data.ThrowIfNull( $"{nameof( ElementDataContainerDto )}.{nameof( ElementDataContainerDto.Data )}" ) );
            }

            return result;
        }
    }
}
