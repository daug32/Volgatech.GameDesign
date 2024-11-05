using System;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Repositories.Dtos.Events
{
    internal static class DataLoadedEventManager
    {
        private static readonly EventManager _eventManager = new();

        public static void Add( Action handler ) => _eventManager.AddWithCommonPriority( handler );
        public static void Trigger() => _eventManager.Trigger();
    }
}