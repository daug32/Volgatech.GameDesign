using System;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Utils;
using UnityEngine.WSA;

namespace Assets.Scripts.Application.Levels
{
    internal class LevelStatistics
    {
        public Atomic ReactionsNumber;
        
        private StoppableTime _gameTime;
        public TimeSpan GameTime { get; private set; } = TimeSpan.MaxValue;

        public static LevelStatistics FromUserLevelData( UserLevelData userLevelData )
        {
            var statistics = new LevelStatistics()
            {
                ReactionsNumber = new Atomic( userLevelData.ReactionsNumber ?? 0 ),
                GameTime = userLevelData.BestCompetitionTime ?? TimeSpan.MaxValue 
            };
            return statistics;
        }

        public void Reset()
        {
            ReactionsNumber = new Atomic();
            _gameTime = StoppableTime.Start();
            GameTime = TimeSpan.MaxValue;
        }

        public void Resume()
        {
            _gameTime.Resume();
        }

        public void Pause()
        {
            _gameTime.Pause();
        }

        public void Commit()
        {
            GameTime = _gameTime.Calculate();
        }
    }
}