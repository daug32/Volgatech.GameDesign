using System;
using System.Collections;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Ui.Books;
using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Repositories.Users;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Ui.Levels
{
    internal class LevelUi
    {
        private readonly GameObjectChildrenContainer _childrenContainer;
        
        public LevelType? CurrentLevel { get; private set; }
        public bool AreInteractionsBlocked { get; private set; }

        public readonly LevelStatistics Statistics = new();

        public readonly Book Book;
        public readonly LevelSettingsUi LevelSettings;
        public readonly LevelCompletedUi LevelCompleted;

        public readonly EventManager OpenLevelSettingsEventManager = new();

        public LevelUi( GameObject gameObject )
        {
            _childrenContainer = new GameObjectChildrenContainer( gameObject );
            Book = new Book( _childrenContainer.Get( "book" ) );
            LevelSettings = new LevelSettingsUi( _childrenContainer.Get( "settings" ) );
            LevelCompleted = new LevelCompletedUi( _childrenContainer.Get( "success" ) );
            _childrenContainer.Get( "settings_button" ).GetComponent<Button>().onClick.AddListener( OpenLevelSettingsEventManager.Trigger );
        }

        public IEnumerator CompleteLevel()
        {
            Debug.Log( "LevelUi: CompleteLevel" );
            Statistics.Commit();
            
            var levelData = LevelDataRepository.Get( CurrentLevel!.Value );
            var userData = UserDataRepository.Get().Arcade[ CurrentLevel!.Value ];
            userData.Apply( levelData, Statistics );
            UserDataRepository.Commit();

            SetElementsInteractionsBlock( true );

            return LevelCompleted.Show( 
                levelData, 
                Statistics );
        }

        public void LoadLevel( LevelType levelType )
        {
            Debug.Log( "LevelUi: LoadLevel" );
            ElementsDataRepository.LoadForLevel( levelType );
            DrawBookElementsHandler.DrawAll();

            CurrentLevel = levelType;
            Statistics.Reset();

            SetElementsInteractionsBlock( false );
            
            _childrenContainer.GameObject.SetActive( true );
        }

        public void UnloadLevel()
        {
            Debug.Log( "LevelUi: UnloadLevel" );
            CurrentLevel = null;
            
            LevelCompleted.Hide();
            InteractiveElementRepository.RemoveAll();
            DrawBookElementsHandler.RemoveAllElements();
            
            SetElementsInteractionsBlock( true );

            _childrenContainer.GameObject.SetActive( false );
        }

        public void SetElementsInteractionsBlock( bool areInteractionsBlocked )
        {
            AreInteractionsBlocked = areInteractionsBlocked;
        }
    }
}