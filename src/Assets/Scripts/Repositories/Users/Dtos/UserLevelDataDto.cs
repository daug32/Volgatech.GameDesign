using System;
using Assets.Scripts.Application.Users;

namespace Assets.Scripts.Repositories.Users.Dtos
{
    public class UserLevelDataDto
    {
        public TimeSpan? BestCompetitionTime { get; set; }

        public UserLevelData Convert()
        {
            return new UserLevelData( BestCompetitionTime );
        }
    }
}