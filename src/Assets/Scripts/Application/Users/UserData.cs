using System.Collections.Generic;
using Assets.Scripts.Application.Levels;

namespace Assets.Scripts.Application.Users
{
    internal class UserData
    {
        public readonly Dictionary<LevelType, UserLevelData> Arcade;

        public UserData(
            Dictionary<LevelType, UserLevelData> arcade )
        {
            Arcade = arcade;
        }
    }
}