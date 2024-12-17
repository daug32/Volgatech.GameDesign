using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models.Rating;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Arcades.Levels
{
    internal class LevelCompletedUi
    {
        private readonly GameObject _gameObject;

        private readonly GameObject _textContainer;
        private readonly GameObject _starsContainer;
        private readonly GameObject _nextLevelButton;

        public readonly EventManager OnOpenMainMenuEvent = new();
        public readonly EventManager OnRestartLevelEvent = new();
        public readonly EventManager OnOpenNextLevelEvent = new();

        public LevelCompletedUi( GameObject gameObject )
        {
            _gameObject = gameObject;

            var childrenManager = new GameObjectChildrenContainer( gameObject );
            _textContainer = childrenManager.Get( "text" );
            _starsContainer = childrenManager.Get( "stars" );

            var buttonsContainer = new GameObjectChildrenContainer( childrenManager.Get( "buttons_container" ) );
            OnOpenMainMenuEvent
               .SubscribeOnClick( buttonsContainer.Get( "exit" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
            OnRestartLevelEvent
               .SubscribeOnClick( buttonsContainer.Get( "restart" ) )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );

            _nextLevelButton = buttonsContainer.Get( "next_level" );
            OnOpenNextLevelEvent
               .SubscribeOnClick( _nextLevelButton )
               .AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );
        }

        public void Show( LevelData levelData, LevelStatistics statistics )
        {
            var levelRating = LevelRating.CompletedLevel( statistics, levelData.Objectives );

            var stars = _starsContainer.FindChildren();
            var currentStar = 0;
            foreach ( var star in stars )
            {
                var spritePath = currentStar < levelRating.StarsAchieved
                    ? "Icons/ui/star"
                    : "Icons/ui/star_unfilled";
                var image = star.GetComponent<Image>();
                image.sprite = Resources.Load<Sprite>( spritePath ).ThrowIfNull( "Failed to load star image" );
                image.preserveAspect = true;

                currentStar++;
            }

            if ( levelRating.IsLevelCompleted )
            {
                _nextLevelButton.SetActive( true );
                _textContainer.GetComponent<TextMeshProUGUI>().text = "Level completed!";
                SoundSourceBehaviour.Instance.PlaySound( SoundType.LevelCompletedSuccess );
            }
            else
            {
                _nextLevelButton.SetActive( false );
                _textContainer.GetComponent<TextMeshProUGUI>().text = "Level failed";
                SoundSourceBehaviour.Instance.PlaySound( SoundType.LevelCompletedFail );
            }
            _gameObject.SetActive( true );
        }

        public void Hide()
        {
            _gameObject.SetActive( false );
        }
    }
}