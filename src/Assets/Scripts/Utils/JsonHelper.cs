using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal static class JsonHelper
    {
        private static readonly JsonSerializerSettings _options = new()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
        };

        public static T Deserialize<T>( string json ) => JsonConvert.DeserializeObject<T>( json, _options );

        public static string Serialize<T>( T obj ) => JsonConvert.SerializeObject( obj, _options );

        public static string LoadJson( string assetPath ) => Resources
            .Load<TextAsset>( assetPath )
            .ThrowIfNull( message: $"Failed to load asset from path. Path: {assetPath}" )
            .text;

        public static string MergeJsons( params string[] jsons )
        {
            if ( jsons.Length < 1 )
            {
                return "{}";
            }

            var mergeOptions = new JsonMergeSettings()
            {
                MergeArrayHandling = MergeArrayHandling.Union
            };

            var first = JObject.Parse( jsons[ 0 ] );
            for ( int i = 1; i < jsons.Length; i++ )
            {
                var second = JObject.Parse( jsons[ i ] );
                first.Merge( second, mergeOptions );
            }

            return first.ToString();
        }
    }
}
