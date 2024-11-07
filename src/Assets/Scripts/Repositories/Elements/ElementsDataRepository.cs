using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Extensions;
using Assets.Scripts.Repositories.Dtos.Elements;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Repositories.Elements
{
    internal static class ElementsDataRepository
    {
        private static Dictionary<ElementId, ElementData> _data;

        public static void LoadForLevel( LevelType levelType )
        {
            string jsonData = JsonHelper.MergeJsons(
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/default" ),
                JsonHelper.LoadJsonIgnoreMissing( $"{Config.ElementsDataDatabase}/{levelType.ToDatabaseFilename()}" ) );
            
            _data = JsonHelper
               .Deserialize<Dictionary<string, ElementDataDto>>( jsonData )
               .ToDictionary( x => new ElementId( x.Key ), x => x.Value.Convert() );
        }

        public static ElementData Get( ElementId id ) => _data[ id ];

        public static List<ElementId> GetAll() => _data.Keys.ToList();

        public static HashSet<ElementId> GetDiscoveredElements() => _data
           .Where( x => x.Value.IsDiscovered )
           .Select( x => x.Key )
           .ToHashSet();

        public static bool Exists( ElementId id ) => _data.ContainsKey( id );

        public static ElementId GetByParents( ElementId firstParent, ElementId secondParent )
        {
            var dataPair = _data.FirstOrDefault( x =>
                    x.Value.Parents.Contains( firstParent )
                    && x.Value.Parents.Contains( secondParent ) );
            return dataPair.Key;
        }
    }
}