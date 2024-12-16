using System;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Utils.Models.Atomics;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Models.Rating
{
    internal class LevelStatistics
    {
        public Atomic ReactionsNumber = new();
        public TimeSpan GameTime = TimeSpan.MaxValue;

        public static LevelStatistics FromUserLevelData( UserLevelData userLevelData ) => new()
        {
            ReactionsNumber = new Atomic( userLevelData.ReactionsNumber ?? 0 ),
            GameTime = userLevelData.BestCompetitionTime ?? TimeSpan.MaxValue
        };

        public void UpdateLevelTime( TimeSpan elapsedTime )
        {
            GameTime = elapsedTime;
        }
    }
}