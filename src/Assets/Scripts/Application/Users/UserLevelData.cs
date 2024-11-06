using System;

namespace Assets.Scripts.Application.Users
{
    public class UserLevelData
    {
        public TimeSpan? BestCompetitionTime;

        public UserLevelData( TimeSpan? bestCompetitionTime = null )
        {
            BestCompetitionTime = bestCompetitionTime;
        }
    }
}