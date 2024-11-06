using System;

namespace Assets.Scripts.Application.Levels.Handlers
{
    public static class LevelCompetitionCalculator
    {
        public static int Calculate( TimeSpan? bestCompetitionTime )
        {
            if ( !bestCompetitionTime.HasValue )
            {
                return 0;
            }

            return 3;
        }
    }
}