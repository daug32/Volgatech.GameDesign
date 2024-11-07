using System;
using System.ComponentModel;
using Assets.Scripts.Application.Levels;
using UnityEngine;

namespace Assets.Scripts.Application.Users
{
    // TODO: Update user data on finishing a level
    internal class UserLevelData
    {
        public int? ReactionsNumber;
        public TimeSpan? BestCompetitionTime;

        public UserLevelData(
            int? reactionsNumber = null,
            TimeSpan? bestCompetitionTime = null )
        {
            ReactionsNumber = reactionsNumber;
            BestCompetitionTime = bestCompetitionTime;
        }

        public void Apply( LevelStatistics statistics )
        {
            var gameTime = statistics.GameTime.Calculate();
            var reactionsNumber = statistics.ReactionsNumber.Get();
            
            bool canUpdate =
                ( !BestCompetitionTime.HasValue || BestCompetitionTime >= gameTime ) &&
                ( !ReactionsNumber.HasValue || ReactionsNumber < reactionsNumber );
            if ( canUpdate )
            {
                ReactionsNumber = reactionsNumber;
                BestCompetitionTime = gameTime;
            }
            
            Debug.Log( this );
        }

        public override string ToString()
        {
            return $"Reactions: {ReactionsNumber}, Time: {BestCompetitionTime}";
        }
    }
}