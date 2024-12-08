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
        public bool AreInteractionsBlocked { get; private set; }

        public readonly LevelStatistics Statistics = new();

        public readonly Book Book;
        public readonly LevelSettingsUi LevelSettings;
        public readonly LevelCompletedUi LevelCompleted;
        public readonly GameObject InteractiveElementsContainer;

        public readonly EventManager OpenLevelSettingsEventManager = new();

        public LevelUi( GameObject gameObject )
        {
            _childrenContainer = new GameObjectChildrenContainer( gameObject );
            Book = new Book( _childrenContainer.Get( "book" ) );
            LevelSettings = new LevelSettingsUi( _childrenContainer.Get( "settings" ) );
            LevelCompleted = new LevelCompletedUi( _childrenContainer.Get( "success" ) );
            InteractiveElementsContainer = _childrenContainer.Get( "interactive_elements_container" );
            _childrenContainer.Get( "settings_button" ).GetComponent<Button>().onClick.AddListener( OpenLevelSettingsEventManager.Trigger );
        }

        public IEnumerator CompleteLevel()
        {
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
            ElementsDataRepository.LoadForLevel( levelType );

            Book.DrawAll();
            CurrentLevel = levelType;
            Statistics.Reset();

            SetElementsInteractionsBlock( false );
            
            _childrenContainer.GameObject.SetActive( true );
        }

        public void UnloadLevel()
        {
            CurrentLevel = null;

            Book.RemoveAll();
            LevelCompleted.Hide();
            
            SetElementsInteractionsBlock( true );

            _childrenContainer.GameObject.SetActive( false );
        }

        public void SetElementsInteractionsBlock( bool areInteractionsBlocked )
        {
            AreInteractionsBlocked = areInteractionsBlocked;
        }
    }
}