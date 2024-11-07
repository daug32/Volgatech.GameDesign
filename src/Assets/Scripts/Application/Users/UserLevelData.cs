using System;

namespace Assets.Scripts.Application.Users
{
    // TODO: Update user data on finishing a level
    public class UserLevelData
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
    }
}