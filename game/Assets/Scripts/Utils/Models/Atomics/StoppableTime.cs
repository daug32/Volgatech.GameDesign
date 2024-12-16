using System;

namespace Assets.Scripts.Utils.Models.Atomics
{
    public class StoppableTime
    {
        private DateTime? _startTime;
        private TimeSpan _elapsedTime;
        private bool _isPaused;

        public StoppableTime( TimeSpan? initialElapsedTime = null )
        {
            _elapsedTime = initialElapsedTime ?? TimeSpan.Zero;
            _startTime = null;
            _isPaused = true;
        }

        /// <summary>
        ///     Pauses the timer, storing the elapsed time up to the pause moment.
        /// </summary>
        public void Pause()
        {
            if ( !_isPaused && _startTime.HasValue )
            {
                _elapsedTime += DateTime.Now - _startTime.Value;
                _startTime = null;
                _isPaused = true;
            }
        }

        /// <summary>
        ///     Resumes the timer from its paused state.
        /// </summary>
        public void Resume()
        {
            if ( _isPaused )
            {
                _startTime = DateTime.Now;
                _isPaused = false;
            }
        }

        /// <summary>
        ///     Resets the timer to its initial state.
        /// </summary>
        public void Reset()
        {
            _elapsedTime = TimeSpan.Zero;
            _startTime = null;
            _isPaused = true;
        }

        /// <summary>
        ///     Calculates the total elapsed time.
        /// </summary>
        /// <returns>A TimeSpan representing the elapsed time.</returns>
        public TimeSpan Calculate()
        {
            if ( !_isPaused && _startTime.HasValue )
            {
                return _elapsedTime + ( DateTime.Now - _startTime.Value );
            }

            return _elapsedTime;
        }
    }
}