using System;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models.Rating;

namespace Assets.Scripts.Application.Users
{
    internal class UserLevelData
    {
        public int? ReactionsNumber;
        public TimeSpan? BestCompetitionTime;

        public bool IsLevelCompleted => ReactionsNumber.HasValue;

        public UserLevelData(
            int? reactionsNumber = null,
            TimeSpan? bestCompetitionTime = null )
        {
            ReactionsNumber = reactionsNumber;
            BestCompetitionTime = bestCompetitionTime;
        }

        public void Apply( LevelData levelData, LevelStatistics statistics )
        {
            var currentRating = IsLevelCompleted
                ? LevelRating.CompletedLevel( LevelStatistics.FromUserLevelData( this ), levelData.Objectives )
                : LevelRating.NotCompletedLevel();
            var newRating = LevelRating.CompletedLevel( statistics, levelData.Objectives );
            
            bool canUpdate = currentRating.StarsAchieved < newRating.StarsAchieved;
            if ( canUpdate )
            {
                ReactionsNumber = statistics.ReactionsNumber.Get();
                BestCompetitionTime = statistics.GameTime;
            }
        }

        public override string ToString()
        {
            return $"Reactions: {ReactionsNumber}, Time: {BestCompetitionTime}";
        }
    }
}