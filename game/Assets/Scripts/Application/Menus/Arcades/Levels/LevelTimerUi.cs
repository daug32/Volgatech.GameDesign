using System;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.Levels
{
    internal class LevelTimerUi
    {
        private readonly GameObject _gameObject;
        private readonly TextMeshPro _timerText;
        private readonly ArcadeTimerBehaviour _timerBehaviour;

        public LevelTimerUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            _timerBehaviour = _gameObject.GetComponent<ArcadeTimerBehaviour>() ?? _gameObject.AddComponent<ArcadeTimerBehaviour>();
            
            var timer = gameObject.FindChild( "text" );
            _timerText = timer.GetComponent<TextMeshPro>();
        }

        public void UpdateTimer( TimeSpan span )
        {
            var totalSeconds = span.TotalSeconds;
            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;
            _timerText.text = $"{minutes}:{seconds}";
        }

        public void PauseTimer()
        {
            _timerBehaviour.StopTimer();
        }

        public void ResumeTimer()
        {
            _timerBehaviour.StartTimer();
        }

        public void SetActive( bool isActive )
        {
            _gameObject.SetActive( isActive );
        }
    }
}