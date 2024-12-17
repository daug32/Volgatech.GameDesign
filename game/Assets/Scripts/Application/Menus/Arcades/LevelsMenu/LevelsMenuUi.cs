using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Assets.Scripts.Application.Menus.Arcades.LevelsMenu.Handlers;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades.LevelsMenu
{
    internal class LevelsMenuUi
    {
        private readonly GameObject _gameObject;

        public readonly GameObject LevelsContainer;
        public readonly GameObject ExampleLevel;
        
        public readonly EventManager<LevelType> OnSelectLevelEvent = new();
        public readonly EventManager OnOpenMainMenuEvent = new();

        public LevelsMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject;

            var childManager = new GameObjectChildrenContainer( gameObject );
            LevelsContainer = childManager.Get( "levels" );
            ExampleLevel = LevelsContainer.FindChild( "example_level" );

            OnSelectLevelEvent.AddWithCommonPriority( _ => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );

            OnOpenMainMenuEvent
               .SubscribeOnClick( childManager.Get( "back_button" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
        }

        public void SetActive( bool isActive )
        {
            _gameObject.SetActive( isActive );

            if ( isActive )
            {
                LevelsMenuDrawer.Draw( this );
            }
            else
            {
                LevelsMenuRemover.Remove( this );
            }
        }
    }
}