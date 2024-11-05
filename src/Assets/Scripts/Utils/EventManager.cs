using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Utils
{
    internal class EventManager<T>
    {
        private class EventListener
        {
            public readonly Guid Id;
            public readonly Action<T> Handler;
            public readonly int Priority;

            public EventListener( Action<T> handler, int priority )
            {
                Id = Guid.NewGuid();
                Handler = handler;
                Priority = priority;
            }
        }
        
        private readonly List<EventListener> _listeners = new();

        public Guid AddWithHighestPriority( Action<T> handler ) => Add( handler, 0 );
        public Guid AddWithCommonPriority( Action<T> handler ) => Add( handler, 1 );
        public Guid AddWithLowestPriority( Action<T> handler ) => Add( handler, 2 );

        public void RemoveListener( Guid subscriptionId )
        {
            var item = _listeners.FirstOrDefault( x => x.Id == subscriptionId );
            if ( item != null ) _listeners.Remove( item );
        }

        public void RemoveAllListeners() => _listeners.Clear();

        public void Trigger( T elementId )
        {
            foreach ( var listener in _listeners.OrderBy( x => x.Priority ) )
            {
                listener.Handler( elementId );
            }
        }

        private Guid Add( Action<T> handler, int priority )
        {
            var listener = new EventListener( handler, priority );
            _listeners.Add( listener );
            return listener.Id;
        }
    }
}