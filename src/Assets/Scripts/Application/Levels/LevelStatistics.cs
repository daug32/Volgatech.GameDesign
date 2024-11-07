using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Levels
{
    internal class LevelStatistics
    {
        public Atomic ReactionsNumber { get; private set; }
        public StoppableTime GameTime { get; private set; }

        public void Reset()
        {
            ReactionsNumber = new Atomic();
            GameTime = StoppableTime.Start();
        }

        public void Start()
        {
            GameTime.Resume();
        }

        public void Stop()
        {
            GameTime.Pause();
        }
    }
}