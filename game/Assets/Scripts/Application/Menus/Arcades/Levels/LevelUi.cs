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
        private readonly GameObjectChildrenContainer _childrenContainer;
        
        public LevelType? CurrentLevel { get; private set; }

        public readonly LevelStatistics Statistics = new();

        public readonly Book Book;
        public readonly LevelSettingsUi LevelSettings;
        public readonly LevelCompletedUi LevelCompleted;
        public readonly LevelTimerUi Timer;

        public readonly EventManager<LevelType> LevelCompletedEventManager = new();
        public readonly EventManager OpenLevelSettingsEventManager = new();

        public LevelUi( GameObject gameObject )
        {
            _childrenContainer = new GameObjectChildrenContainer( gameObject );

            Book = new Book( _childrenContainer.Get( "book" ) );
            Book.OnElementCreated.AddWithCommonPriority( element =>
            {
                Statistics.ReactionsNumber.Increment();

                LevelData levelData = LevelDataRepository.Get( CurrentLevel!.Value );
                if ( levelData.IsLevelCompleted( ElementsDataRepository.GetDiscoveredElements() ) )
                {
                    LevelCompletedEventManager.Trigger( CurrentLevel.Value );
                }
            } );

            LevelSettings = new LevelSettingsUi( _childrenContainer.Get( "settings" ) );
            LevelCompleted = new LevelCompletedUi( _childrenContainer.Get( "success" ) );
            _childrenContainer.Get( "settings_button" ).GetComponent<Button>().onClick.AddListener( OpenLevelSettingsEventManager.Trigger );
            Timer = new LevelTimerUi( _childrenContainer.Get( "timer" ) );
        }

        public IEnumerator CompleteLevel()
        {
            Statistics.Commit();
            Timer.PauseTimer();
            
            var levelData = LevelDataRepository.Get( CurrentLevel!.Value );
            var userData = UserDataRepository.Get().Arcade[ CurrentLevel!.Value ];
            userData.Apply( levelData, Statistics );
            UserDataRepository.Commit();
            
            ElementsInteractionBlocker.BlockInteractions();

            return LevelCompleted.Show( 
                levelData, 
                Statistics );
        }
        
        public void LoadLevel( LevelType levelType )
        {
            ElementsDataRepository.LoadForLevel( levelType );
            
            ElementsInteractionBlocker.AllowInteractions();

            Book.Load();
            CurrentLevel = levelType;
            Timer.SetActive( true );
            Statistics.Reset();
            Timer.ResumeTimer();
            
            _childrenContainer.GameObject.SetActive( true );
        }

        public void UnloadLevel()
        {
            CurrentLevel = null;

            Book.Unload();
            LevelCompleted.Hide();
            Timer.PauseTimer();
            Timer.SetActive( false );
            
            _childrenContainer.GameObject.SetActive( false );
        }

        public bool IsActive => _childrenContainer.GameObject.activeSelf;
    }
}