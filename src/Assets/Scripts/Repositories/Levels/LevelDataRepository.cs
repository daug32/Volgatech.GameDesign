using System;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Repositories.Dtos.Events;
using UnityEngine;

namespace Assets.Scripts.Repositories.Levels
{
    internal static class LevelDataRepository
    {
        private static LevelData _levelData;

        static LevelDataRepository()
        {
            LoadData();
            DataLoadedEventManager.Add( LoadData );
        }

        public static LevelData Get() => _levelData;

        private static void LoadData() => _levelData = DataRepository.Get().Level.Convert();
    }
}