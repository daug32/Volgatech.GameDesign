using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Utils
{
    public class StoppableTime
    {
        private readonly DateTime _start;
        private DateTime? _end;
        private readonly List<(DateTime blockStart, DateTime? blockEnd)> _blocks = new();
        public bool IsPaused { get; private set; }

        private StoppableTime( TimeSpan addIntoStatistics )
        {
            _start = DateTime.Now.Subtract( addIntoStatistics );
        }

        public static StoppableTime Start( TimeSpan? addIntoStatistics = null )
        {
            return new StoppableTime( addIntoStatistics ?? TimeSpan.Zero );
        }

        public StoppableTime Pause()
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
            
            return this;
        }

        public StoppableTime Resume()
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
            
            return this;
        }

        public StoppableTime Commit()
        {
            _end = DateTime.Now;
            return this;
        }

        public TimeSpan Calculate()
        {
            var end = _end ?? DateTime.Now;

            var marks = _blocks
               .SelectMany( x => new[] { x.blockStart, x.blockEnd } )
               .Where( x => x.HasValue )
               .Select( x => x.Value )
               .OrderBy( x => x )
               .ToList();
            marks.Add( end );

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