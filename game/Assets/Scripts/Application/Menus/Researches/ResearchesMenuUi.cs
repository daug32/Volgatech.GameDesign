using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Application.Menus.Researches.Handlers;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Researches
{
    public class ResearchesMenuUi
    {
        private readonly GameObject _gameObject;

        public readonly GameObject ElementsContainer;

        public readonly EventManager OnOpenMainMenuEvent = new();

        public ResearchesMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );

            var childManager = new GameObjectChildrenContainer( gameObject );
            ElementsContainer = childManager
               .Get( "elements_container" )
               .FindChild( "viewport" )
               .FindChild( "content" )
               .ThrowIfNull( message: "Failed to find elements_container in scroll view" );
            OnOpenMainMenuEvent
               .SubscribeOnClick( childManager.Get( "settings_button" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
        }

        public void SetActive( bool isActive )
        {
            if ( isActive )
            {
                ResearchMenuDrawer.Draw( this );
            }
            else
            {
                ResearchesRemover.Remove( this );
            }
            
            _gameObject.SetActive( isActive );
        }
    }
}