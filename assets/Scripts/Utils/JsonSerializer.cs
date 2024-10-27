using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Assets.Scripts.Utils
{
    internal static class JsonSerializer
    {
        private static readonly JsonSerializerOptions _serializationOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static T Deserialize<T>( string json ) => System.Text.Json.JsonSerializer.Deserialize<T>( json, _serializationOptions );

        public static string Serialize<T>( T obj ) => System.Text.Json.JsonSerializer.Serialize( obj, _serializationOptions );
    }
}
