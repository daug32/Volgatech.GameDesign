using Assets.Scripts.Application.Levels;

namespace Assets.Scripts.Repositories.Levels
{
    internal static class LevelDataRepository
    {
        private static readonly LevelData _levelData = DataRepository.Get().Level.Convert();

        public static LevelData Get() => _levelData;
    }
}