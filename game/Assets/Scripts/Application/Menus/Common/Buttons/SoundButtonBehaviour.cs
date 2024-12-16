using System.IO;
using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.Menus.Sandbox;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Common.Buttons
{
    public class SoundButtonBehaviour : MonoBehaviour
    {
        public readonly EventManager OnCLick = new();
        
        private void Start()
        {
            SetUpComponents();
        }

        private void SetUpComponents()
        {
            var btnImage = gameObject.GetComponent<Image>() ?? gameObject.AddComponent<Image>();
            UpdateIcon();
            
            var btnComponent = gameObject.GetComponent<Button>() ?? gameObject.AddComponent<Button>();
            btnComponent.onClick.AddListener( OnCLick.Trigger );
            btnComponent.targetGraphic = btnImage;

            OnCLick.AddWithHighestPriority( SoundSettings.SwitchSound );
            SoundSettings.OnSoundStateChangedEvent.AddWithCommonPriority( UpdateIcon );
        }

        private void UpdateIcon()
        {
            var image = gameObject.GetComponent<Image>();

            var sprite = SoundSettings.IsSoundEnabled
                ? Resources.Load<Sprite>( "Icons/ui/sound" )
                : Resources.Load<Sprite>( "Icons/ui/sound_no_sound" );
            if ( sprite is null )
            {
                throw new FileNotFoundException( "Failed to load sound icon" );
            }

            image.sprite = sprite;
            image.preserveAspect = true;
        }
    }
}