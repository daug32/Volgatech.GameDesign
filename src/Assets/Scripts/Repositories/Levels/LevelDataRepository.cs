using Assets.Scripts.Application.Levels;
using Assets.Scripts.Repositories.Dtos.Events;

namespace Assets.Scripts.Repositories.Levels
{
    internal static class LevelDataRepository
    {
        private static LevelData _currentLevelData;
        
        static LevelDataRepository()
        {
            LoadData();
            DataLoadedEventManager.Add( LoadData );
        }

        public static LevelData Get() => _currentLevelData;

        private static void LoadData() => _currentLevelData = DataRepository.Get().Level.Convert();
    }
}