using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Users;

namespace Assets.Scripts.Repositories.Users.Dtos
{
    internal class UserDataDto
    {
        public Dictionary<string, UserLevelDataDto> Arcade { get; set; }

        public UserData Convert()
        {
            var arcade = Arcade.ToDictionary(
                x => Enum.TryParse( x.Key, true, out LevelType result )
                    ? result
                    : throw new ArgumentException( $"Filed to parse level type from user data dto. Given value: {x.Key}" ),
                x => x.Value.Convert() );
            return new UserData( arcade );
        }
    }
}