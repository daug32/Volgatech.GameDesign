using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Common.Books.Elements;

namespace Assets.Scripts.Application.Users.Repositories.Dtos
{
    internal class UserDataDto
    {
        public Dictionary<string, UserLevelDataDto> Arcade { get; set; }
        public List<string> DiscoveredElements { get; set; }

        public UserData Convert()
        {
            var arcade = Arcade.ToDictionary(
                x => Enum.TryParse( x.Key, true, out LevelType result )
                    ? result
                    : throw new ArgumentException( $"Filed to parse level type from user data dto. Given value: {x.Key}" ),
                x => x.Value.Convert() );
            
            foreach ( LevelType levelType in Enum.GetValues( typeof( LevelType ) ) )
            {
                if ( !arcade.ContainsKey( levelType ) )
                {
                    arcade.Add( levelType, new UserLevelData() );
                }
            }

            DiscoveredElements ??= new List<string>();

            return new UserData( arcade, DiscoveredElements.Select( x => new ElementId( x ) ).ToHashSet() );
        }
    }
}