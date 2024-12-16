using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Rating;
using Assets.Scripts.Utils;
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
            OnOpenMainMenuEvent.SubscribeOnClick( buttonsContainer.Get( "exit" ) );
            OnRestartLevelEvent.SubscribeOnClick( buttonsContainer.Get( "restart" ) );
            _nextLevelButton = buttonsContainer.Get( "next_level" );
            OnOpenNextLevelEvent.SubscribeOnClick( _nextLevelButton );
        }

        public void Show( LevelData levelData, LevelStatistics statistics )
        {
            LevelRating levelRating = LevelRating.CompletedLevel( statistics, levelData.Objectives );
            
            var stars = _starsContainer.FindChildren();
            int currentStar = 0;
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

            _nextLevelButton.SetActive( levelRating.IsLevelCompleted );
            _textContainer.GetComponent<TextMeshProUGUI>().text = levelRating.IsLevelCompleted ? "Level completed!" : "Level failed"; 

            _gameObject.SetActive( true );
        }

        public void Hide()
        {
            _gameObject.SetActive( false );
        }
    }
}