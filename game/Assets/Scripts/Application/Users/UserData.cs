using System.Collections.Generic;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Common.Books.Elements;

namespace Assets.Scripts.Application.Users
{
    internal class UserData
    {
        public readonly Dictionary<LevelType, UserLevelData> Arcade;
        public readonly HashSet<ElementId> DiscoveredElements;

        public UserData( Dictionary<LevelType, UserLevelData> arcade, HashSet<ElementId> discoveredElements )
        {
            DiscoveredElements = discoveredElements;
            Arcade = arcade;
        }
    }
}