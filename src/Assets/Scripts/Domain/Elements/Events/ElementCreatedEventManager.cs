using System;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Domain.Elements.Events
{
    internal static class ElementCreatedEventManager
    {
        private static readonly EventManager<ElementId> _eventManager = new();

        public static Guid AddWithHighestPriority( Action<ElementId> handler ) => _eventManager.AddWithHighestPriority( handler ); 
        public static Guid AddWithCommonPriority( Action<ElementId> handler ) => _eventManager.AddWithCommonPriority( handler ); 
        public static Guid AddWithLowestPriority( Action<ElementId> handler ) => _eventManager.AddWithLowestPriority( handler ); 
        public static void RemoveListener( Guid subscriptionId ) => _eventManager.RemoveListener( subscriptionId ); 
        public static void Trigger( ElementId elementId ) => _eventManager.Trigger( elementId ); 
    }
}