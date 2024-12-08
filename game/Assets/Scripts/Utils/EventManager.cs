using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Utils
{
    internal class EventManager
    {
        private class EventListener
        {
            public readonly Guid Id;
            public readonly Action Handler;
            public readonly int Priority;

            public EventListener( Action handler, int priority )
            {
                Id = Guid.NewGuid();
                Handler = handler;
                Priority = priority;
            }
        }
        
        private readonly List<EventListener> _listeners = new();

        public Guid AddWithHighestPriority( Action handler ) => Add( handler, 0 );
        public Guid AddWithCommonPriority( Action handler ) => Add( handler, 1 );
        public Guid AddWithLowestPriority( Action handler ) => Add( handler, 2 );

        public void RemoveListener( Guid subscriptionId )
        {
            var item = _listeners.FirstOrDefault( x => x.Id == subscriptionId );
            if ( item != null ) _listeners.Remove( item );
        }

        public void RemoveAllListeners() => _listeners.Clear();

        public void Trigger()
        {
            foreach ( var listener in _listeners.OrderBy( x => x.Priority ) )
            {
                listener.Handler();
            }
        }

        private Guid Add( Action handler, int priority )
        {
            var listener = new EventListener( handler, priority );
            _listeners.Add( listener );
            return listener.Id;
        }
    }
}