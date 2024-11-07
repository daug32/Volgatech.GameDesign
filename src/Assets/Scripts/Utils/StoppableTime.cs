using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class StoppableTime
    {
        private readonly DateTime _start;
        private DateTime? _end;
        private readonly List<(DateTime blockStart, DateTime? blockEnd)> _blocks = new();
        public bool IsPaused { get; private set; }

        private StoppableTime() => _start = DateTime.Now;
        public static StoppableTime Start() => new();

        public void Pause()
        {
            if ( _end != null )
            {
                throw new InvalidOperationException( "Can't pause time because it was completely stopped" );
            }
            
            if ( IsPaused )
            {
                throw new InvalidOperationException( "Time was already paused and can not be paused again" );
            }

            _blocks.Add( ( DateTime.Now, null ) );
            IsPaused = true;
        }

        public void Resume()
        {
            if ( _end != null )
            {
                throw new InvalidOperationException( "Can't resume time because it was completely stopped" );
            }
            
            if ( !IsPaused )
            {
                throw new InvalidOperationException( "Time was not paused and can not be resumed" );
            }

            _blocks[ ^1 ] = ( _blocks[ ^1 ].blockStart, DateTime.Now );
            IsPaused = false;
        }

        public TimeSpan Calculate()
        {
            _end = DateTime.Now;

            var marks = _blocks
               .SelectMany( x => new[] { x.blockStart, x.blockEnd } )
               .Where( x => x.HasValue )
               .Select( x => x.Value )
               .OrderBy( x => x )
               .ToList();
            marks.Add( _end.Value );

            var lastMark = _start;
            bool shouldCalculate = true;
            
            var allTime = TimeSpan.Zero;

            foreach ( var mark in marks )
            {
                if ( shouldCalculate )
                {
                    var period = mark.Subtract( lastMark );
                    allTime = allTime.Add( period );
                }

                shouldCalculate = !shouldCalculate;
                lastMark = mark;
            }

            return allTime;
        }
    }
}