using System.IO;
using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Utils.Models.Events;
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
            Image image = CreateOrUpdateIcon();
            
            var btnComponent = gameObject.GetComponent<Button>() ?? gameObject.AddComponent<Button>();
            btnComponent.onClick.AddListener( OnCLick.Trigger );
            btnComponent.targetGraphic = image;

            OnCLick.AddWithHighestPriority( SoundSettings.SwitchSound );
            SoundSettings.OnSoundStateChangedEvent.AddWithCommonPriority( () => CreateOrUpdateIcon() );
        }

        private Image CreateOrUpdateIcon()
        {
            var image = gameObject.GetComponent<Image>() ?? gameObject.AddComponent<Image>();

            var sprite = SoundSettings.IsSoundEnabled
                ? Resources.Load<Sprite>( "Icons/ui/sound" )
                : Resources.Load<Sprite>( "Icons/ui/sound_no_sound" );
            if ( sprite is null )
            {
                throw new FileNotFoundException( "Failed to load sound icon" );
            }

            image.sprite = sprite;
            image.preserveAspect = true;

            return image;
        }
    }
}