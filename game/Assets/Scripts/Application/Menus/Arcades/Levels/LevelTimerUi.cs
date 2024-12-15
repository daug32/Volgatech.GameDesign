using System;
using Assets.Scripts.Application.Menus.Arcades.Levels.Behaviours;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.Levels
{
    internal class LevelTimerUi
    {
        private readonly GameObject _gameObject;
        private readonly ArcadeTimerBehaviour _timerBehaviour;

        public LevelTimerUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            _timerBehaviour = _gameObject.GetComponent<ArcadeTimerBehaviour>() ?? _gameObject.AddComponent<ArcadeTimerBehaviour>();
        }

        public void ResetTimer() => _timerBehaviour.ResetTimer();
        public void PauseTimer() => _timerBehaviour.PauseTimer();
        public void ResumeTimer() => _timerBehaviour.ResumeTimer();
        public TimeSpan GetElapsedTime() => _timerBehaviour.GetElapsedTime();

        public void SetActive( bool isActive )
        {
            _gameObject.SetActive( isActive );
        }
    }
}