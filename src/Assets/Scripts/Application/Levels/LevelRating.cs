using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Application.Levels
{
    internal class LevelRating
    {
        public readonly int StarsAchieved;

        private LevelRating( int starsAchieved )
        {
            StarsAchieved = starsAchieved;
        }

        public static LevelRating CompletedLevel( 
            LevelStatistics statistics, 
            IEnumerable<LevelObjective> objectives )
        {
            var achievedStars = objectives.Count( objective => IsObjectiveCompleted( statistics, objective ) );
            return new LevelRating( achievedStars );
        }

        public static LevelRating NotCompletedLevel()
        {
            return new LevelRating( 0 );
        }

        private static bool IsObjectiveCompleted( LevelStatistics statistics, LevelObjective objective )
        {
            if ( objective.Type == LevelObjectiveType.FinishByReactions )
            {
                return
                    statistics.ReactionsNumber.Get() > 0 &&
                    statistics.ReactionsNumber.Get() <= Int32.Parse( objective.Data );
            }

            if ( objective.Type == LevelObjectiveType.FinishByTime )
            {
                return statistics.GameTime <= TimeSpan.Parse( objective.Data );
            }

            throw new ArgumentOutOfRangeException( $"Uknown {nameof( LevelObjectiveType )}. Given value: {objective.Type}" );
        }
    }
}