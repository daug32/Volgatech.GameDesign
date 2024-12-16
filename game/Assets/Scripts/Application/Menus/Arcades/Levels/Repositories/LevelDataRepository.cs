using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Arcades.Levels.Repositories.Dtos;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Menus.Arcades.Repositories
{
    internal static class LevelDataRepository
    {
        private static Dictionary<LevelType, LevelData> _levels;

        public static void Load()
        {
            if ( _levels != null )
            {
                return;
            }
            
            var jsonData = JsonHelper.LoadJson( $"{Config.LevelDataDatabase}/levels" );
            
            _levels = JsonHelper
               .Deserialize<Dictionary<string, LevelDataDto>>( jsonData )
               .ToDictionary(
                    x =>
                    {
                        if ( !Enum.TryParse( x.Key, true, out LevelType levelType ) )
                        {
                            throw new ArgumentException( $"Failed to parse level type. Value: {x.Key}" );
                        }
                        return levelType;
                    },
                    x => x.Value.Convert() );
        }

        public static LevelData Get( LevelType levelType ) => _levels[ levelType ];
    }
}