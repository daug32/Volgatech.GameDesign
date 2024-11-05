using System;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Domain.Levels
{
    internal static class LevelCompletedEventManager
    {
        private static readonly EventManager<LevelType> _eventManager = new();

        public static Guid Add( Action<LevelType> handler ) => _eventManager.AddWithCommonPriority( handler ); 
        public static void Trigger( LevelType LevelType ) => _eventManager.Trigger( LevelType );
        public static void RemoveAllListeners() => _eventManager.RemoveAllListeners();
    }
}