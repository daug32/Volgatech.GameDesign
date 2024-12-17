using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.GameSettings.Sounds;
using Assets.Scripts.Application.Menus.Arcades.Levels.Behaviours;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models.Rating;
using Assets.Scripts.Application.Menus.Arcades.Levels.Repositories;
using Assets.Scripts.Application.Menus.Common.Books;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Application.Users.Repositories;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Arcades.Levels
{
    internal class LevelUi
    {
        private readonly GameObject _gameObject;
        
        public LevelType? CurrentLevel { get; private set; }
        public LevelStatistics Statistics;

        public readonly Book Book;
        public readonly LevelSettingsUi LevelSettings;
        public readonly LevelCompletedUi LevelCompleted;
        public readonly LevelTimerUi Timer;

        public readonly EventManager<LevelType> OnLevelCompletedEvent = new();
        public readonly EventManager OnOpenSettingsEvent = new();
        public readonly EventManager OnOpenMainMenuEvent = new();

        public LevelUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );

            var childrenContainer = new GameObjectChildrenContainer( gameObject );
            
            // Statistics
            Timer = new LevelTimerUi( childrenContainer.Get( "timer" ) );
            Statistics = new LevelStatistics();

            // Elements
            Book = new Book( childrenContainer.Get( "book" ) );
            Book.OnElementCreationSuccessEvent.AddWithCommonPriority( _ =>
            {
                Statistics.ReactionsNumber.Increment();

                if ( !LevelDataRepository.Get( CurrentLevel!.Value ).IsLevelCompleted( Book.DiscoveredElements ) )
                {
                    return;
                }

                OnLevelCompletedEvent.Trigger( CurrentLevel.Value );
            } );

            // Level settings
            LevelSettings = new LevelSettingsUi( childrenContainer.Get( "settings" ) );
            LevelSettings.OnCloseSettingsEvent.AddWithCommonPriority( HideSettings );
            LevelSettings.OnOpenMainMenuEvent.AddWithCommonPriority( OnOpenMainMenuEvent.Trigger );
            LevelSettings.OnRestartLevelEvent.AddWithCommonPriority( () => LoadLevel( CurrentLevel.ThrowIfNull( "Can't restart because current level is not loaded" ) ) );
            OnOpenSettingsEvent.SubscribeOnClick( childrenContainer.Get( "settings_button" ) );
            OnOpenSettingsEvent.AddWithCommonPriority( ShowSettings );
            OnOpenSettingsEvent.AddWithCommonPriority( () => SoundSourceBehaviour.Instance.PlaySound( SoundType.UiButtonPress ) );

            // Level completed
            LevelCompleted = new LevelCompletedUi( childrenContainer.Get( "success" ) );
            LevelCompleted.OnRestartLevelEvent.AddWithCommonPriority( () => LoadLevel( CurrentLevel.ThrowIfNull( "Can't restart because current level is not loaded" ) ) );
            LevelCompleted.OnOpenMainMenuEvent.AddWithCommonPriority( OnOpenMainMenuEvent.Trigger );
            LevelCompleted.OnOpenNextLevelEvent.AddWithCommonPriority( LoadNextLevel );
            OnLevelCompletedEvent.AddWithCommonPriority( currentLevel => CompleteLevel() );
            OnOpenMainMenuEvent.AddWithCommonPriority( () => LevelCompleted!.Hide() );
        }

        public void LoadLevel( LevelType levelType )
        {
            ElementsDataRepository.LoadForLevel( levelType );
            ElementsInteractionBlocker.AllowInteractions();
            
            LevelCompleted.Hide();
            LevelSettings.Hide();

            Book.Load( LevelDataRepository.Get( levelType ).StartElements );
            CurrentLevel = levelType;
            Statistics = new LevelStatistics();
            Timer.SetActive( true );
            Timer.ResetTimer();
            
            _gameObject.SetActive( true );
        }

        public void UnloadLevel()
        {
            CurrentLevel = null;

            Book.Unload();
            LevelCompleted.Hide();
            Timer.PauseTimer();
            Timer.SetActive( false );
            Statistics = null;
            
            _gameObject.SetActive( false );
        }

        private void CompleteLevel()
        {
            Timer.PauseTimer();
            Statistics.UpdateLevelTime( Timer.GetElapsedTime() );
            
            var levelData = LevelDataRepository.Get( CurrentLevel!.Value );
            var userData = UserDataRepository.Get().Arcade[ CurrentLevel!.Value ];
            userData.Apply( levelData, Statistics );
            UserDataRepository.Commit();
            
            ElementsInteractionBlocker.BlockInteractions();

            LevelCompleted.Show( levelData, Statistics );
        }

        private void LoadNextLevel()
        {
            var nextLevel = LevelSuggester.SuggestNextLevel( CurrentLevel.ThrowIfNull( "Can't get to next level because current level is not loaded" ) );
            if ( nextLevel is null )
            {
                OnOpenMainMenuEvent.Trigger();
                return;
            }
            
            LoadLevel( nextLevel.Value );
        }

        private void ShowSettings()
        {
            Timer.PauseTimer();
            ElementsInteractionBlocker.BlockInteractions();
            LevelSettings.Show( CurrentLevel!.Value );
        }

        private void HideSettings()
        {
            LevelSettings.Hide();
            ElementsInteractionBlocker.AllowInteractions();
            Timer.ResumeTimer();
        }
    }
}