using System;

namespace Assets.Scripts.Application.Users
{
    // TODO: Update user data on finishing a level
    public class UserLevelData
    {
        public TimeSpan? BestCompetitionTime;

        public UserLevelData( TimeSpan? bestCompetitionTime = null )
        {
            BestCompetitionTime = bestCompetitionTime;
        }
    }
}