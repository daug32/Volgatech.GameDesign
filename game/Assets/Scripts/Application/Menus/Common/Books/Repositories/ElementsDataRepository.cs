using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Extensions;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Application.Menus.Common.Books.Repositories.Dtos;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Menus.Common.Books.Repositories
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

        public static bool Exists( ElementId id ) => _data.ContainsKey( id );

        public static ElementId GetByParents( ElementId firstParent, ElementId secondParent )
        {
            var expectedParents = new HashSet<ElementId>()
            {
                firstParent,
                secondParent
            };

            foreach ( (ElementId elementId, ElementData elementData) in _data )
            {
                foreach ( var parents in elementData.Parents )
                {
                    if ( parents.SetEquals( expectedParents ) )
                    {
                        return elementId;
                    }
                }
            }

            return null;
        }
    }
}