using System;

namespace Assets.Scripts.Application.Users.Repositories.Dtos
{
    internal class UserLevelDataDto
    {
        public int? ReactionsNumber { get; set; }
        public TimeSpan? BestCompetitionTime { get; set; }

        public UserLevelData Convert() => new(
            ReactionsNumber,
            BestCompetitionTime );
    }
}