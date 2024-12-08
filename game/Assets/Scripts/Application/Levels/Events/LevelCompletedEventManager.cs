using System;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Levels.Events
{
    internal static class LevelCompletedEventManager
    {
        private static readonly EventManager _eventManager = new();

        public static void Add( Action func ) => _eventManager.AddWithCommonPriority( func );
        public static void Trigger() => _eventManager.Trigger();
    }
}