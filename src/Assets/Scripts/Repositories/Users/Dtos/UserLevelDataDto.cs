using System;
using Assets.Scripts.Application.Users;

namespace Assets.Scripts.Repositories.Users.Dtos
{
    public class UserLevelDataDto
    {
        public int? ReactionsNumber { get; set; }
        public TimeSpan? BestCompetitionTime { get; set; }

        public UserLevelData Convert() => new(
            ReactionsNumber,
            BestCompetitionTime );
    }
}