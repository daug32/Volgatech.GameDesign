using System.Collections;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.Levels
{
    public class ArcadeTimerBehaviour : MonoBehaviour
    {
        private TextMeshProUGUI _timerText;
        private Coroutine _updateTimerCoroutine;

        private void Start()
        {
            _timerText = gameObject.FindChild( "text" )?.GetComponent<TextMeshProUGUI>();

            if ( _timerText == null )
            {
                Debug.LogError( "TextMeshProUGUI component not found!" );
                return;
            }

            StartTimer();
        }

        public void StartTimer()
        {
            if ( _updateTimerCoroutine == null )
            {
                Debug.Break();
                _updateTimerCoroutine = StartCoroutine( UpdateTimer() );
            }
        }

        public void StopTimer()
        {
            if ( _updateTimerCoroutine != null )
            {
                StopCoroutine( _updateTimerCoroutine );
                _updateTimerCoroutine = null;
            }
        }

        private IEnumerator UpdateTimer()
        {
            while ( true )
            {
                var level = UiItemsRepository.GetUserInterface().Menu.ArcadeMenu.Level;

                if ( level.IsActive && !level.LevelSettings.IsActive )
                {
                    var gameTime = level.Statistics.GameTime;
                    var totalSeconds = gameTime.TotalSeconds;
                    var minutes = ( int )totalSeconds / 60;
                    var seconds = ( int )totalSeconds % 60;

                    _timerText.text = $"{minutes:00}:{seconds:00}";
                }

                yield return new WaitForSeconds( 1f );
            }
        }
    }
}