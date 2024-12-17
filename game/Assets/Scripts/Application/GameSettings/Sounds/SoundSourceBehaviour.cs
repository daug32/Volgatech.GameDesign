using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Application.GameSettings.Sounds
{
    public class SoundSourceBehaviour : MonoBehaviour
    {
        private static Dictionary<SoundType, AudioClip> _sounds;
        public static SoundSourceBehaviour Instance { get; private set; }
        
        private AudioSource _audioSource;

        private void Awake()
        {
            if ( Instance == null )
            {
                Instance = this;
                _audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.spatialBlend = 0;
                _sounds = BuildSounds();
            }
        }

        public void PlaySound( SoundType soundType )
        {
            if ( SoundSettings.IsSoundEnabled )
            {
                StopSound();

                _audioSource.volume = soundType == SoundType.ElementCreationFailed ? 0.2f : 1;
                
                _audioSource.PlayOneShot( _sounds[ soundType ] );
            }
        }

        public void StopSound()
        {
            _audioSource.Stop();
        }

        private static Dictionary<SoundType, AudioClip> BuildSounds()
        {
            return new Dictionary<SoundType, AudioClip>
            {
                { SoundType.ElementAppeared, Resources.Load<AudioClip>( "Sounds/element_appearing" ) },
                { SoundType.ElementCreationSuccess, Resources.Load<AudioClip>( "Sounds/element_creation_success" ) },
                { SoundType.ElementCreationFailed, Resources.Load<AudioClip>( "Sounds/element_creation_failed" ) },
                { SoundType.LevelCompletedFail, Resources.Load<AudioClip>( "Sounds/level_completed_failed" ) },
                { SoundType.LevelCompletedSuccess, Resources.Load<AudioClip>( "Sounds/level_completed_success" ) },
                { SoundType.UiButtonPress, Resources.Load<AudioClip>( "Sounds/ui_button_press" ) },
            };
        }
    }
}