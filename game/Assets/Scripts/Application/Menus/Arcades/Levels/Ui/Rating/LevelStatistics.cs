using System;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Rating
{
    internal class LevelStatistics
    {
        public Atomic ReactionsNumber;
        
        private StoppableTime _gameTime;
        public TimeSpan GameTime => _gameTime.Calculate();

        public static LevelStatistics FromUserLevelData( UserLevelData userLevelData ) => new()
        {
            ReactionsNumber = new Atomic( userLevelData.ReactionsNumber ?? 0 ),
            _gameTime = StoppableTime.Start( userLevelData.BestCompetitionTime ?? TimeSpan.MaxValue ).Commit()
        };

        public void Reset()
        {
            ReactionsNumber = new Atomic();
            _gameTime = StoppableTime.Start();
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
            _gameTime.Commit();
        }
    }
}