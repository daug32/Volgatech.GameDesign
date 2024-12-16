using System;
using System.Collections;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Atomics;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Behaviours
{
    public class ArcadeTimerBehaviour : MonoBehaviour
    {
        private StoppableTime _stoppableTime;
        private TextMeshProUGUI _timerText;
        private Coroutine _updateTimerCoroutine;

        private void Start()
        {
            _stoppableTime = new StoppableTime();
            _timerText = gameObject.FindChild( "text" ).GetComponent<TextMeshProUGUI>();
            ResumeTimer();
        }

        private void Update()
        {
            if ( _needToRetryCoroutineStart )
            {
                ResumeTimer();
            }
        }

        public void ResetTimer()
        {
            _stoppableTime = new StoppableTime();
            ResumeTimer();
        }

        private bool _needToRetryCoroutineStart = false; 
        public void ResumeTimer()
        {
            if ( _updateTimerCoroutine == null )
            {
                _stoppableTime.Resume();
                bool canSetCoroutine = gameObject.activeSelf && gameObject.activeInHierarchy;
                if ( !canSetCoroutine )
                {
                    _needToRetryCoroutineStart = true;
                    return;
                }
                
                _updateTimerCoroutine = StartCoroutine( UpdateTimer() );
                _needToRetryCoroutineStart = false;
            }
        }

        public void PauseTimer()
        {
            if ( _updateTimerCoroutine != null )
            {
                _stoppableTime.Pause();
                StopCoroutine( _updateTimerCoroutine );
                _updateTimerCoroutine = null;
            }
        }

        public TimeSpan GetElapsedTime() => _stoppableTime.Calculate();

        private IEnumerator UpdateTimer()
        {
            while ( true )
            {
                var gameTime = _stoppableTime.Calculate();

                var totalSeconds = gameTime.TotalSeconds;
                var minutes = ( int )totalSeconds / 60;
                var seconds = ( int )totalSeconds % 60;

                _timerText.text = $"{minutes:00}:{seconds:00}";

                yield return new WaitForSeconds( 1f );
            }
        }
    }
}