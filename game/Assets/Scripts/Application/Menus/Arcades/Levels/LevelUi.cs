using System.Collections;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Rating;
using Assets.Scripts.Application.Menus.Arcades.Repositories;
using Assets.Scripts.Application.Menus.Common.Books;
using Assets.Scripts.Application.Menus.Common.Books.Handlers;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Application.Users.Repositories;
using Assets.Scripts.Utils;
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

        public readonly EventManager<LevelType> LevelCompletedEventManager = new();

        public LevelUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            var childrenContainer = new GameObjectChildrenContainer( gameObject );
            
            // Statistics
            Timer = new LevelTimerUi( childrenContainer.Get( "timer" ) );
            Statistics = new LevelStatistics();

            // Elements
            Book = new Book( childrenContainer.Get( "book" ) );
            Book.OnElementCreated.AddWithCommonPriority( _ =>
            {
                Statistics.ReactionsNumber.Increment();

                if ( !LevelDataRepository.Get( CurrentLevel!.Value ).IsLevelCompleted( ElementsDataRepository.GetDiscoveredElements() ) )
                {
                    return;
                }

                LevelCompletedEventManager.Trigger( CurrentLevel.Value );
            } );

            // Level settings
            LevelSettings = new LevelSettingsUi( childrenContainer.Get( "settings" ) );
            LevelSettings.GetToLevelEventManager.AddWithCommonPriority( HideSettings );
            childrenContainer.Get( "settings_button" ).GetComponent<Button>().onClick.AddListener( ShowSettings );

            // Level completed
            LevelCompleted = new LevelCompletedUi( childrenContainer.Get( "success" ) );
        }

        public IEnumerator CompleteLevel()
        {
            Timer.PauseTimer();
            Statistics.UpdateLevelTime( Timer.GetElapsedTime() );
            
            var levelData = LevelDataRepository.Get( CurrentLevel!.Value );
            var userData = UserDataRepository.Get().Arcade[ CurrentLevel!.Value ];
            userData.Apply( levelData, Statistics );
            UserDataRepository.Commit();
            
            ElementsInteractionBlocker.BlockInteractions();

            return LevelCompleted.Show( levelData, Statistics );
        }
        
        public void LoadLevel( LevelType levelType )
        {
            ElementsDataRepository.LoadForLevel( levelType );
            ElementsInteractionBlocker.AllowInteractions();

            Book.Load();
            CurrentLevel = levelType;
            Statistics = new LevelStatistics();
            Timer.SetActive( true );
            Timer.ResetTimer();
            
            _gameObject.SetActive( true );
        }

        public void ShowSettings()
        {
            Timer.PauseTimer();
            ElementsInteractionBlocker.BlockInteractions();
            LevelSettings.ShowSettings( CurrentLevel!.Value );
        }

        public void HideSettings()
        {
            LevelSettings.HideSettings();
            ElementsInteractionBlocker.AllowInteractions();
            Timer.ResumeTimer();
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
    }
}